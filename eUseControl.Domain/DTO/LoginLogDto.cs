namespace eUseControl.Domain.DTOs;

public class CreateLoginLogDto
{
    public string UserIp { get; set; } = string.Empty;
    public string LoginIp { get; set; } = string.Empty;
    public DateTime LoginDataTime { get; set; }
}

public class UpdateLoginLogDto
{
    public string UserIp { get; set; } = string.Empty;
    public string LoginIp { get; set; } = string.Empty;
    public DateTime LoginDataTime { get; set; }
}

public class LoginLogResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string UserIp { get; set; } = string.Empty;
    public string LoginIp { get; set; } = string.Empty;
    public DateTime LoginDataTime { get; set; }
}
