using ExpenseTrackerApp.Backend.Expense.Domain.User;
using static ExpenseTrackerApp.Backend.Expense.Domain.Enum.AppEnums;

namespace ExpenseTrackerApp.Backend.Expense.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public TransactionType TransactionType { get; set; }  // Income / Expense
        public PaymentMode PaymentMode { get; set; }          // Cash / Bank / Card
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Relationships
        public ApplicationUser User { get; set; }
        public Category Category { get; set; }

    }

}
