namespace eUseControl.Domain.Entities.services;

public class ServiceBookingData
{
    public string Id { get; set; } = string.Empty;
    //все про запись на услуги имя описание и т. д.
    public string BookingId { get; set; } = string.Empty;
    public string BookingName { get; set; } = string.Empty;
    public string BookingDescription { get; set; } = string.Empty;
    //время записи
    public DateTime BookingTime { get; set; }
    public DateTime BookingDate { get; set; }
}