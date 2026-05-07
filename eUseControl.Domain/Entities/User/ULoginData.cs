namespace eUseControl.Domain.Entities.User;
// по сути жунал куда будут записываться данные про последний вход где и когда
public class ULoginData
{
    public string Id { get; set; } // id самой записи лога
    public string UserIp { get; set; }
    public string LoginIp { get; set; }
    public DateTime LoginDataTime { get; set; }
    
}