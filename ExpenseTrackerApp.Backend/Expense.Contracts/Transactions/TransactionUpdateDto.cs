using System.ComponentModel.DataAnnotations;
using static ExpenseTrackerApp.Backend.Expense.Domain.Enum.AppEnums;

namespace ExpenseTrackerApp.Backend.Expense.Contracts.Transactions
{
    public class TransactionUpdateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }  // Credit or Debit

        [Required]
        public PaymentMode PaymentMode { get; set; }          // Cash / Bank / Card

        [Required]
        public decimal Amount { get; set; }

        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}
