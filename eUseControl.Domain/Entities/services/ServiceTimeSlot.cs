namespace eUseControl.Domain.Entities.services;

public class ServiceTimeSlot
{
    public string Id { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsAvailable { get; set; } =  true;
    
    
    //внешние ключи
    public string SpecialistId { get; set; } = string.Empty;
    public string SpecialistName { get; set; } = string.Empty;

    //когда слот на бронирование услуги занят - ссылка на бронирование 
    public string? BookingId { get; set; } = string.Empty;
    public ServiceBookingData? Booking { get; set; }
}