namespace SecondhandStore.Extension;

public class EmailSettingModel
{
    public static EmailSettingModel Instance { get; set; }
    public string FromEmailAddress { get; set; }
    public string FromDisplayName { get; set; }
    public Smtp Smtp { get; set; }
    
}

public class Smtp
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
    public bool UseCredential { get; set; }
}