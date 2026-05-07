namespace eUseControl.Domain.Entities.Specialist;

public class SpecialistWorkSchedule
{
    public string Id { get; set; } = string.Empty;
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    
    public string SpecialistId { get; set; } = string.Empty;
    public SpecialistData SpecialistData { get; set; } = new SpecialistData();
}