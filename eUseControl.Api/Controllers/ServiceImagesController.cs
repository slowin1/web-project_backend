using eUseControl.BussinessLogic.Functions.ServiceImages;
using eUseControl.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceImagesController : ControllerBase
{
    private readonly IServiceImageFlow _serviceImageFlow;

    public ServiceImagesController(IServiceImageFlow serviceImageFlow)
    {
        _serviceImageFlow = serviceImageFlow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _serviceImageFlow.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var image = await _serviceImageFlow.GetByIdAsync(id);
        return image is null ? NotFound() : Ok(image);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceImageDto dto) => Ok(await _serviceImageFlow.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateServiceImageDto dto)
    {
        var image = await _serviceImageFlow.UpdateAsync(id, dto);
        return image is null ? NotFound() : Ok(image);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => await _serviceImageFlow.DeleteAsync(id) ? NoContent() : NotFound();
}
