namespace eUseControl.Domain.Entities.User;
// по сути жунал куда будут записываться данные про последний вход где и когда
public class ULoginData
{
    public string Id { get; set; } = string.Empty; // id самой записи лога
    public string UserIp { get; set; } = string.Empty;
    public string LoginIp { get; set; } = string.Empty;
    public DateTime LoginDataTime { get; set; }
}