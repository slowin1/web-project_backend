using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using BC = BCrypt.Net.BCrypt;

namespace eUseControl.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMongoCollection<UserModel> _users;

    public AuthController(IMongoDatabase database)
    {
        _users = database.GetCollection<UserModel>("users");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) ||
            string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Password))
        {
            return BadRequest(new { message = "All fields are required" });
        }

        var existingUser = await _users.Find(u =>
            u.Username.ToLower() == dto.Username.ToLower() ||
            u.Email.ToLower() == dto.Email.ToLower())
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            return BadRequest(new { message = "User already exists" });
        }

        var user = new UserModel
        {
            Id = ObjectId.GenerateNewId(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BC.HashPassword(dto.Password),
            IsEmailVerified = false,
            CreatedAt = DateTime.UtcNow
        };

        await _users.InsertOneAsync(user);

        return Ok(new
        {
            message = "User registered successfully"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _users.Find(u => u.Username == dto.Username)
            .FirstOrDefaultAsync();

        if (user == null || !BC.Verify(dto.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        return Ok(new
        {
            token = "test-token-123",
            user = new
            {
                id = user.Id.ToString(),
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
    public async Task<IActionResult> GetUsers()
    {
        var users = await _users.Find(_ => true)
            .ToListAsync();

        var result = users.Select(u => new
        {
            id = u.Id.ToString(),
            u.Username,
            u.Email,
            u.IsEmailVerified,
            u.CreatedAt
        });

        return Ok(result);
    }
}

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}

public class RegisterRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ForgotPasswordDto
{
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordDto
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}