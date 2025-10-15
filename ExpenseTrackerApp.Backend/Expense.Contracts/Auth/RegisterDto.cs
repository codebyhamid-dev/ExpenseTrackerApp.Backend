using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApp.Backend.Expense.Contracts.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
