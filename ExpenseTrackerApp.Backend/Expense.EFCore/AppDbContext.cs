using ExpenseTrackerApp.Backend.Expense.Domain;
using ExpenseTrackerApp.Backend.Expense.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerApp.Backend.Expense.EFCore
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ------------------------
            // Transaction
            // ------------------------
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions");
                // Description max length 500
                entity.Property(t => t.Description)
                      .HasMaxLength(500);
                // Amount precision
                entity.Property(t => t.Amount)
                      .HasPrecision(18, 2);
                // One user → many transactions
                entity.HasOne(t => t.User)
                      .WithMany(u => u.Transactions)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
