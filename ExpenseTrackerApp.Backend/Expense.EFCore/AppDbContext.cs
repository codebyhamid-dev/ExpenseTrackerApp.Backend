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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");

                entity.Property(u => u.Name)
                      .HasMaxLength(100)
                      .IsRequired(false);

                // Store Base64 image without length limit
                entity.Property(u => u.ProfilePicUrl)
                      .HasColumnType("TEXT"); // or "nvarchar(max)" for SQL Server
            });

            // ------------------------
            // Category
            // ------------------------
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                // Name is required and max length 100
                entity.Property(c => c.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                // One user → many categories
                entity.HasOne(c => c.User)
                      .WithMany(u => u.Categories)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

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

                // One category → many transactions
                entity.HasOne(t => t.Category)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
