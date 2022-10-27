
using ProCodeGuide.Samples.Hangfire.Model;

namespace ProCodeGuide.Samples.Hangfire.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
