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
        public Task<TransactionReadDto> CreateTransactionAsync(TransactionCreateDto transactionCreateDto)
        {
            throw new NotImplementedException();
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
