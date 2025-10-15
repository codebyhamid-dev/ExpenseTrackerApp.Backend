using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApp.Backend.Expense.Contracts.Auth
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }
    }
}
