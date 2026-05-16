using eUseControl.BussinessLogic.Functions.ServiceBookings;
using eUseControl.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceBookingsController : ControllerBase
{
    private readonly IServiceBookingFlow _serviceBookingFlow;

    public ServiceBookingsController(IServiceBookingFlow serviceBookingFlow)
    {
        _serviceBookingFlow = serviceBookingFlow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _serviceBookingFlow.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var booking = await _serviceBookingFlow.GetByIdAsync(id);
        return booking is null ? NotFound() : Ok(booking);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceBookingDto dto) => Ok(await _serviceBookingFlow.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateServiceBookingDto dto)
    {
        var booking = await _serviceBookingFlow.UpdateAsync(id, dto);
        return booking is null ? NotFound() : Ok(booking);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => await _serviceBookingFlow.DeleteAsync(id) ? NoContent() : NotFound();
}
