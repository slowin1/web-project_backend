using eUseControl.Domain.Entities.services;

namespace eUseControl.Domain.Entities.Specialist;

public class SpecialistData
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    
    //на случай если мастер вышел в отпуск
    public bool IsActive { get; set; } = true;
    
    //какие услуги умеет делать мастер
    public ICollection<ServiceData> Services { get; set; } = new List<ServiceData>();
    public ICollection<ServiceTimeSlot> TimeSlots { get; set; } = new List<ServiceTimeSlot>();
}