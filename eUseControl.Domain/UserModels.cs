namespace eUseControl.Domain;

public class ULoginData
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ULoginResp
{
    public bool Status { get; set; }
    public string StatusMsg { get; set; } = string.Empty;
}
