using Microsoft.AspNetCore.Mvc;

namespace eUseControl.api.controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    [HttpGet ("service")]
    public IActionResult Service()
    {
        return Ok("привет");
    }
}