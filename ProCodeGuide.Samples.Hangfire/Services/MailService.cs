using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ProCodeGuide.Samples.Hangfire.Model;
using ProCodeGuide.Samples.Hangfire.Settings;

namespace ProCodeGuide.Samples.Hangfire.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            using (SmtpClient SmtpServer = new())
            {
                try
                {
                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                    email.Subject = mailRequest.Subject;
                    var builder = new BodyBuilder();
                    builder.HtmlBody = mailRequest.Body;
                    email.Body = new TextPart(TextFormat.Html) { Text = mailRequest.Body };

                    await SmtpServer.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    await SmtpServer.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);

                    await SmtpServer.SendAsync(email);
                    _logger.LogInformation("Send Email");
                    await SmtpServer.DisconnectAsync(true);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Send Email Exception: " + ex.Message);
                    await SmtpServer.DisconnectAsync(true);
                }
            }
        }
    }
}