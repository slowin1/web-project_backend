using System.ComponentModel.DataAnnotations;


namespace eUseControl.Domain.Entities.services;

public class ServiceBookingData
{
    [Key]
    public string Id { get; set; } = string.Empty;
    //все про запись на услуги имя описание и т. д.
    [Required]
    public string BookingId { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string BookingName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200, MinimumLength = 30)]
    public string BookingDescription { get; set; } = string.Empty;
    //время записи
    [Required]
    [DataType(DataType.Date)]
    public DateTime BookingTime { get; set; }
    public DateTime BookingDate { get; set; }
}