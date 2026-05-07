namespace eUseControl.Domain.Entities.services;

public class ServiceData
{
    public string Id { get; set; } = string.Empty;
    public string NameOfService { get; set; } = string.Empty;//название услуги
    public string NameOfMaster { get; set; } = string.Empty; //Имя мастера который будет оказывать услугу
    public string DescriptionOfService { get; set; } = string.Empty;//описание услуги
    public int DurationOfService { get; set; }
    public Decimal PriceOfService { get; set; } 
    
    //связь с категорией 
    public string CategoryId { get; set; } = string.Empty;
    public ServiceCategoryData Category { get; set; } = null!;
}