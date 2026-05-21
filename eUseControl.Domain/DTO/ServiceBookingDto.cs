namespace eUseControl.Domain.DTOs;

public class CreateServiceBookingDto
{
    public string BookingId { get; set; } = string.Empty;
    public string BookingName { get; set; } = string.Empty;
    public string BookingDescription { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public DateTime BookingDate { get; set; }
}

public class UpdateServiceBookingDto
{
    public string BookingId { get; set; } = string.Empty;
    public string BookingName { get; set; } = string.Empty;
    public string BookingDescription { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public DateTime BookingDate { get; set; }
}

public class ServiceBookingResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string BookingId { get; set; } = string.Empty;
    public string BookingName { get; set; } = string.Empty;
    public string BookingDescription { get; set; } = string.Empty;
    public DateTime BookingTime { get; set; }
    public DateTime BookingDate { get; set; }
}
