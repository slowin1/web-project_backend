using Microsoft.AspNetCore.Mvc;
using eUseControl.Api.DTO.Auth;

namespace eUseControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto dto)
    {
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        return Ok();
    }

    [HttpPost("forgot-password")]
    public IActionResult ForgotPassword([FromBody] ForgotPassDto dto)
    {
        return Ok();
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPassword dto)
    {
        return Ok();
    }
}