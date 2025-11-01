using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerApp.Backend.Expense.Domain.User
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string Name { get; set; }

        // ✅ Make this nullable
        public string? ProfilePicUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow; // User creation time

        // Refresh Token fields
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Relationships
        public ICollection<Transaction>? Transactions { get; set; }

    }
}
