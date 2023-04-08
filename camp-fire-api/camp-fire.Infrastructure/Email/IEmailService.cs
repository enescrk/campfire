namespace camp_fire.Infrastructure.Email;

public interface IEmailService
{
    Task<bool> SendEmailAsync(SendEmailModel sendMailModel);
}
