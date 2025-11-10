using AutoMapper;
using ExpenseTrackerApp.Backend.Expense.Contracts.Transactions;
using ExpenseTrackerApp.Backend.Expense.EFCore;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<TransactionReadDto>> GetAllTransactionsAsync()
        {
            var transactions=await _context.Transactions.ToListAsync(); 
            if(transactions==null || transactions.Count==0)
            {
                return new List<TransactionReadDto>();
            }
            var result=_mapper.Map<List<TransactionReadDto>>(transactions);
            return result;
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
