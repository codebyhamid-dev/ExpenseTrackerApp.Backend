using ExpenseTrackerApp.Backend.Expense.Contracts.Auth;
using ExpenseTrackerApp.Backend.Expense.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTrackerApp.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        // ------------------------
        // POST: api/auth/register
        // ------------------------
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

        // ------------------------
        // POST: api/auth/login
        // ------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //validate user credentials
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized(new { message = "Invalid email or password." });

            // Generate tokens
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Save refresh token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                success = true,
                message = "User logged in successfully.",
                Accesstoken = jwtToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = user.RefreshTokenExpiryTime, // send expiry
                user = new
                {
                    userId= user.Id,    
                    name = user.Name,
                    email = user.Email,
                }
            });
        }
        //generate access token
        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim("name", user.Name ?? ""),
                new Claim("username", user.UserName ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //generate refresh token
        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
      
        // ------------------------
        // POST: api/auth/logout
        // ------------------------
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
        {
            if (string.IsNullOrEmpty(dto.RefreshToken))
                return BadRequest(new { message = "Refresh token is required." });

            // find user with this refresh token
            var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == dto.RefreshToken);

            if (user == null)
                return Unauthorized(new { message = "Invalid refresh token." });

            // remove refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                success = true,
                message = "User logged out successfully."
            });
        }


    }
}
