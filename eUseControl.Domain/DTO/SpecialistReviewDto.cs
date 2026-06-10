namespace eUseControl.Domain.DTOs;

public class CreateSpecialistReviewDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string BookingId { get; set; } = string.Empty;
    public string? SpecialistId { get; set; }
}

public class UpdateSpecialistReviewDto
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string BookingId { get; set; } = string.Empty;
    public string? SpecialistId { get; set; }
}

public class SpecialistReviewResponseDto
{
    public string Id { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string ClientId { get; set; } = string.Empty;
    public string BookingId { get; set; } = string.Empty;
    public string? SpecialistId { get; set; }
    public string? UserName { get; set; }
}
