namespace eUseControl.Domain.Entities.services;

public class ServiceCategoryData
{
    public string Id { get; set; } = string.Empty;
    public string NameOfCategory { get; set; } = string.Empty;
    //к примеру на сайте захотят удалить эту слуги или она будет не доступна
    public bool IsActive { get; set; } = true;
    //все услуги данный категории 
    public ICollection<ServiceData> Services { get; set; } = new List<ServiceData>();
}