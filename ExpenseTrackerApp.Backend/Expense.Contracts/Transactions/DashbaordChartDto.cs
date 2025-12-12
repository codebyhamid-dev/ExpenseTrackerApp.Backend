namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public class DashbaordChartDto
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
    }
}
