using System.ComponentModel.DataAnnotations;
using static ExpenseTrackerApp.Backend.Expense.Domain.Enum.AppEnums;

namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public class TransactionReadDto
    {
        public Guid? UserId { get; set; }
        public TransactionType TransactionType { get; set; }  // credit / debit 
        public PaymentMode PaymentMode { get; set; }       // Cash / Bank / Card
        public string? Category { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
    }
}
