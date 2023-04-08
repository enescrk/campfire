namespace camp_fire.Infrastructure.Email;

public class SendEmailModel
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? Attachment { get; set; }
    public List<string>? To { get; set; }
    public List<string>? Bcc { get; set; }
    public List<string>? Cc { get; set; }
}
