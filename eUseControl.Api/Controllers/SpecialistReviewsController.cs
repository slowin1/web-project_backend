using eUseControl.BussinessLogic.Functions.SpecialistReviews;
using eUseControl.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace eUseControl.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialistReviewsController : ControllerBase
{
    private readonly ISpecialistReviewFlow _specialistReviewFlow;

    public SpecialistReviewsController(ISpecialistReviewFlow specialistReviewFlow)
    {
        _specialistReviewFlow = specialistReviewFlow;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _specialistReviewFlow.GetAllAsync());

    [HttpGet("specialist/{specialistId}")]
    public async Task<IActionResult> GetBySpecialist(string specialistId) =>
        Ok(await _specialistReviewFlow.GetBySpecialistAsync(specialistId));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var review = await _specialistReviewFlow.GetByIdAsync(id);
        return review is null ? NotFound() : Ok(review);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecialistReviewDto dto)
    {
        try
        {
            return Ok(await _specialistReviewFlow.CreateAsync(dto));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateSpecialistReviewDto dto)
    {
        try
        {
            var review = await _specialistReviewFlow.UpdateAsync(id, dto);
            return review is null ? NotFound() : Ok(review);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => await _specialistReviewFlow.DeleteAsync(id) ? NoContent() : NotFound();
}
