using Microsoft.AspNetCore.Mvc;
using eUseControl.Api.DTO.Auth;

namespace eUseControl.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public AuthController()
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        return Ok(new { message = "Registration is temporarily disabled (PostgreSQL migration in progress)" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        return Ok(new { message = "Login is temporarily disabled (PostgreSQL migration in progress)" });
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword([FromBody] ForgotPassDto dto)
    {
        return Ok(new
        {
            message = "Password reset link has been sent to your email"
        });
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPassword dto)
    {
        return Ok(new
        {
            message = "Password reset successful"
        });
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(new[] { new { message = "Database is disconnected" } });
    }
}



