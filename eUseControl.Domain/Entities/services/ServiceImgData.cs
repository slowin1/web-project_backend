namespace eUseControl.Domain.Entities.services;

public class ServiceImgData
{
    public string Id { get; set; } = string.Empty; 
    //cloudinary там будут храниться изображения
    public string ImageUrl { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string ServiceId { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
