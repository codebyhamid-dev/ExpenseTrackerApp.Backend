namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public interface ITransactionAppService
    {
        Task<List<TransactionReadDto>> GetAllTransactionsAsync();
        Task<TransactionReadDto> GetTransactionByIdAsync(Guid transactionId);
        Task<TransactionReadDto> CreateTransactionAsync(TransactionCreateDto transactionCreateDto);
        Task<TransactionReadDto> UpdateTransactionAsync(Guid transactionId, TransactionUpdateDto transactionUpdateDto);
        Task DeleteTransactionAsync(Guid transactionId);
    }
}
