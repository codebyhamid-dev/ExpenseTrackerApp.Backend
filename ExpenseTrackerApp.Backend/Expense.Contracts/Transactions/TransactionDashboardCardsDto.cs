namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public class TransactionDashboardCardsDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
        public int TotalTransactions { get; set; }
    }
}
