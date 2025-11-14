using AutoMapper;
using ExpenseTrackerApp.Backend.Expense.Contracts.Transactions;
using ExpenseTrackerApp.Backend.Expense.EFCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ExpenseTrackerApp.Backend.Expense.Services.Transaction
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TransactionAppService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TransactionReadDto> CreateTransactionAsync(TransactionCreateDto transactionCreateDto)
        {
            // Map DTO → Domain model
            var transaction = _mapper.Map<Domain.Entities.Transaction>(transactionCreateDto);
            transaction.Id = Guid.NewGuid();
            transaction.CreatedAt = DateTimeOffset.UtcNow;

            // Save to database
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            // Map Domain → Read DTO
            var result = _mapper.Map<TransactionReadDto>(transaction);
            return result;
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            var transaction=await _context.Transactions.FirstOrDefaultAsync(t=>t.Id==transactionId);
            if(transaction!=null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<TransactionReadDto>> GetAllTransactionsAsync(TransactionInputDto inputDto)
        {
            var transactions = _context.Transactions.AsQueryable();

            // 🔍 SIMPLE TEXT SEARCH (Category + Description)
            if (!string.IsNullOrWhiteSpace(inputDto.Search))
            {
                var keyword = inputDto.Search.ToLower();
                transactions = transactions.Where(t =>
                    (t.Category != null && t.Category.ToLower().Contains(keyword))
                );
            }
            // 🎯 CATEGORY FILTER
            if (!string.IsNullOrWhiteSpace(inputDto.Category))
            {
                string cat = inputDto.Category.ToLower();
                transactions = transactions.Where(t =>
                    t.Category != null && t.Category.ToLower() == cat
                );
            }
            // 💰 MIN AMOUNT FILTER
            if (inputDto.MinAmount.HasValue)
                transactions = transactions.Where(t => t.Amount >= inputDto.MinAmount.Value);

            // 💰 MAX AMOUNT FILTER
            if (inputDto.MaxAmount.HasValue)
                transactions = transactions.Where(t => t.Amount <= inputDto.MaxAmount.Value);

            // ➕➖ TYPE (Credit / Debit)
            if (inputDto.TransactionType.HasValue)
                transactions = transactions.Where(t => t.TransactionType == inputDto.TransactionType);

            // 💳 PAYMENT MODE: Cash / Bank / Card
            if (inputDto.PaymentMode.HasValue)
                transactions = transactions.Where(t => t.PaymentMode == inputDto.PaymentMode);

            // 📌 SORTING BY CREATED TIME (default: newest first)
            transactions = inputDto.SortDescending
                ? transactions.OrderByDescending(t => t.CreatedAt)
                : transactions.OrderBy(t => t.CreatedAt);

            // ✔ Total
            int totalCount = await transactions.CountAsync();
            // 📄 PAGINATION
            var items = await transactions
                .Skip(inputDto.SkipCount)
                .Take(inputDto.MaxResultCount)
                .ToListAsync();

            return new PagedResultDto<TransactionReadDto>
            {
                Items = _mapper.Map<List<TransactionReadDto>>(items),
                TotalCount = totalCount
            };
        }

        public async Task<TransactionReadDto> GetTransactionByIdAsync(Guid transactionId)
        {
            var transaction=await _context.Transactions.FirstOrDefaultAsync(t=>t.Id==transactionId);
            if(transaction==null)
            {
                return null;
            }
            var result=_mapper.Map<TransactionReadDto>(transaction);
            return result;
        }

        public async Task<TransactionReadDto> UpdateTransactionAsync(Guid transactionId, TransactionUpdateDto transactionUpdateDto)
        {
            var transaction=await _context.Transactions.FirstOrDefaultAsync(t=>t.Id==transactionId);
            if(transaction==null)
            {
                return null;
            }
            // Map updated fields from DTO to Domain model
            _mapper.Map(transactionUpdateDto, transaction);
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
            var result=_mapper.Map<TransactionReadDto>(transaction);
            return result;
        }
    }
}
