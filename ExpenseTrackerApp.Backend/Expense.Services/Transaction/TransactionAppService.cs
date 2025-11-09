using AutoMapper;
using ExpenseTrackerApp.Backend.Expense.Contracts.Transactions;
using ExpenseTrackerApp.Backend.Expense.EFCore;

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

        public Task DeleteTransactionAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TransactionReadDto>> GetAllTransactionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransactionReadDto> GetTransactionByIdAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionReadDto> UpdateTransactionAsync(Guid transactionId, TransactionUpdateDto transactionUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
