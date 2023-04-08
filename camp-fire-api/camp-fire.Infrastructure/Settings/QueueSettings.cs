namespace camp_fire.Infrastructure.Settings;

public class QueueSettings
{
    public IList<string> HostNames { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}