using ExpenseTrackerApp.Backend.Expense.Domain.User;
using System.ComponentModel.DataAnnotations;
using static ExpenseTrackerApp.Backend.Expense.Domain.Enum.AppEnums;

namespace ExpenseTrackerApp.Backend.Expense.Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }
        public Guid? CategoryId { get; set; }

        public CategoryType TransactionType { get; set; }  // Income / Expense
        public PaymentMode PaymentMode { get; set; }       // Cash / Bank / Card

        [Required]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Relationships
        public ApplicationUser? User { get; set; }
        public Category? Category { get; set; }

    }

}
