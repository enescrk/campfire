using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using camp_fire.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace camp_fire.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly IOptions<EmailSettings> _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings;
        _logger = logger;
    }

    /// <summary>
    /// Kullanıcıya mail gönderilmesi.
    /// </summary>
    /// <param name="sendMailModel"></param>
    /// <returns></returns>
    public async Task<bool> SendEmailAsync(SendEmailModel sendMailModel)
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors) { return true; };

        var port = Convert.ToInt16(_emailSettings.Value.Port);
        var smtpEmailHost = _emailSettings.Value.SmtpEmailHost;
        var userName = _emailSettings.Value.EmailAddress;
        var password = _emailSettings.Value.Password;

        using (var client = new SmtpClient
        {
            EnableSsl = true,
            Port = port,
            UseDefaultCredentials = false,
            Host = smtpEmailHost!,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            DeliveryFormat = SmtpDeliveryFormat.International,
            Credentials = new NetworkCredential
            {
                UserName = userName,
                Password = password
            }
        })
        {
            var emailMessage = new MailMessage
            {
                IsBodyHtml = true,
                Body = sendMailModel.Body,
                Subject = sendMailModel.Subject,
                From = new MailAddress(userName!)
            };

            try
            {
                if (!string.IsNullOrEmpty(sendMailModel.Attachment))
                {
                    emailMessage.Attachments.Add(new Attachment(sendMailModel.Attachment));
                }

                if (sendMailModel.Cc != null)
                {
                    foreach (var emailCc in sendMailModel.Cc)
                    {
                        emailMessage.CC.Add(emailCc);
                    }
                }

                if (sendMailModel.Bcc != null)
                {
                    foreach (var emailBcc in sendMailModel.Bcc)
                    {
                        emailMessage.Bcc.Add(emailBcc);
                    }
                }

                if (sendMailModel.To != null)
                {
                    foreach (var emailTo in sendMailModel.To)
                    {
                        emailMessage.To.Add(emailTo);
                    }
                }
                else
                {
                    _logger.LogError("Email gönderilemedi!");
                    return false;
                }

                await client.SendMailAsync(emailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Email gönderme metodunda hata oluştu!", ex.Message);
                return false;
            }
        }
    }
}
