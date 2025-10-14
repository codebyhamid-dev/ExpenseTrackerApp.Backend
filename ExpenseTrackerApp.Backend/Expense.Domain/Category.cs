using ExpenseTrackerApp.Backend.Expense.Domain.User;
using System.ComponentModel.DataAnnotations;
using static ExpenseTrackerApp.Backend.Expense.Domain.Enum.AppEnums;

namespace ExpenseTrackerApp.Backend.Expense.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        // Nullable — null = system category (shared by everyone), non-null = user custom category
        public Guid? UserId { get; set; }
        [Required]
        public string Name { get; set; }           // Example: "Food", "Salary", "Transport"
        public CategoryType Type { get; set; }     // Income or Expense
        public string Icon { get; set; }           // Optional emoji/icon for UI

        // Relationships
        public ApplicationUser User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
