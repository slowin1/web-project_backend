using eUseControl.Domain.Entities.services;
using eUseControl.Domain.Entities.User;

namespace eUseControl.Domain.Entities.Specialist;

public class SpecialistReview
{
    public string Id { get; set; } = string.Empty;
    public int Rating { get; set; } 
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    
    //
    public string ClientId { get; set; } = string.Empty;
    public UserData Client { get; set; } = null!;
    
    public string BookingId { get; set; } = string.Empty;
    public ServiceBookingData? Booking { get; set; } = null!;
    
}