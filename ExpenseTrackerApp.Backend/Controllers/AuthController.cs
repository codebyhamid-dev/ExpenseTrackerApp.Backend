using ExpenseTrackerApp.Backend.Expense.Contracts.Auth;
using ExpenseTrackerApp.Backend.Expense.Domain.User;
using ExpenseTrackerApp.Backend.Expense.EFCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ Manual check for matching passwords
            if (dto.Password != dto.ConfirmPassword)
                return BadRequest(new { message = "Password and Confirm Password do not match." });

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "A user with this email already exists." });

            // Create a new ApplicationUser
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                Name = dto.Name,
            };

            // Create the user using ASP.NET Core Identity
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "User creation failed", errors });
            }

            return Ok(new { message = "User registered successfully" });

        }
    }
}
