using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerApp.Backend.Expense.Domain.User
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string ProfilePicUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        // Relationships
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
