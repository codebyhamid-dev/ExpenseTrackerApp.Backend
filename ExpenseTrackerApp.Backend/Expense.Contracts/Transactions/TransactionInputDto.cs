using ExpenseTrackerApp.Backend.Expense.Domain.Enum;
using static ExpenseTrackerApp.Backend.Expense.Domain.Enum.AppEnums;

namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public class TransactionInputDto
    {
        public string? Search { get; set; }  // simple text search category

        public string? Category { get; set; }

        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }

        public TransactionType? TransactionType { get; set; }  // credit / debit 
        public PaymentMode? PaymentMode { get; set; }

        public bool SortDescending { get; set; } = true; // sort by CreatedAt

        public int SkipCount { get; set; } = 0;
        public int MaxResultCount { get; set; } = 10;
    }
}
