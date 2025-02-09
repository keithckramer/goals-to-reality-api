using GoalsToRealityAPI.Data;
using GoalsToRealityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoalsToRealityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration; // For JWT secret key

        public AuthController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            // Check if the email already exists
            if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest(new { Message = "Email already exists" });

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Create the user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword, // Save hashed password
                Address = request.Address,
                City = request.City,
                StateID = request.StateID,
                Zip = request.Zip,
                Birthday = request.Birthday,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // Find the user
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized(new { Message = "Invalid email or password" });

            // Verify the password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized(new { Message = "Invalid email or password" });

            // Generate a JWT token
            var token = GenerateJwtToken(user);

            return Ok(new AuthResponse { Message = "Login successful", Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
