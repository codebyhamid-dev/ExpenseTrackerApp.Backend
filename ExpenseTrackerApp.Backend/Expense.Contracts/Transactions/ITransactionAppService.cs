namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public interface ITransactionAppService
    {
        Task<PagedResultDto<TransactionReadDto>> GetAllTransactionsAsync(TransactionInputDto inputDto);
        Task<TransactionReadDto> GetTransactionByIdAsync(Guid transactionId);
        Task<TransactionReadDto> CreateTransactionAsync(TransactionCreateDto transactionCreateDto);
        Task<TransactionReadDto> UpdateTransactionAsync(Guid transactionId, TransactionUpdateDto transactionUpdateDto);
        Task DeleteTransactionAsync(Guid transactionId);
        Task<TransactionDashboardCardsDto> GetTransactionDashboardCardsAsync();
        Task<List<DashbaordChartDto>> GetDashbaordChartAsync();
    }
}
