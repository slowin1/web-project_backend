using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace eUseControl.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly List<UserModel> Users = new();

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) ||
                string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "All fields are required" });
            }

            var existingUser = Users.FirstOrDefault(u =>
                u.Username.ToLower() == dto.Username.ToLower() ||
                u.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            var user = new UserModel
            {
                Id = Users.Count + 1,
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };

            Users.Add(user);

            return Ok(new
            {
                message = "User registered successfully"
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            var user = Users.FirstOrDefault(u =>
                u.Username == dto.Username &&
                u.Password == dto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {
                token = "test-token-123",
                user = new
                {
                    id = user.Id,
                    username = user.Username,
                    email = user.Email
                }
            });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            return Ok(new
            {
                message = "Password reset link has been sent to your email"
            });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            return Ok(new
            {
                message = "Password reset successful"
            });
        }
        
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var result = Users.Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.Password,
            });

            return Ok(result);
        }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegisterRequestDto
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; } = "";
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; } = "";
        public string Token { get; set; } = "";
        public string NewPassword { get; set; } = "";
    }
}