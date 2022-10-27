using Microsoft.Extensions.Options;
using ProCodeGuide.Samples.Hangfire.Model;
using ProCodeGuide.Samples.Hangfire.Settings;

namespace ProCodeGuide.Samples.Hangfire.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly TaskSettings _taskSettings;
        private readonly ILogger<HealthCheckService> _logger;
        private readonly IMailService _mailService;

        public HealthCheckService(IOptions<TaskSettings> taskSettings, ILogger<HealthCheckService> logger, IMailService mailService)
        {
            _taskSettings = taskSettings.Value;
            _logger = logger;
            _mailService = mailService;
        }

        public async Task HealthCheck(string backGroundJobType, string startTime)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    _logger.LogInformation("service check");
                    HttpResponseMessage response = await client.GetAsync(_taskSettings.ServiceUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("service response success request url:" + _taskSettings.ServiceUrl);
                    }
                    else
                    {
                        #region Email Request

                        MailRequest mailRequest = new MailRequest();
                        mailRequest.ToEmail = _taskSettings.ToMail;//"devops@mucitpanda.com";
                        mailRequest.Subject = "HangFire Service Health Check";
                        mailRequest.Body = "service response failed!  <br>  Statu Code: " + response.StatusCode + " <br> Content: " + response.Content;
                        await _mailService.SendEmailAsync(mailRequest);
                        #endregion
                        _logger.LogInformation("service response !  <br>  Statu Code: " + response.StatusCode + " <br> Content: " + response.Content);
                    }
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Service HealthCheck request url:" + _taskSettings.ServiceUrl + " Exception: " + ex.Message);

                    #region Email Request

                    MailRequest mailRequest = new MailRequest();
                    mailRequest.ToEmail = _taskSettings.ToMail;
                    mailRequest.Subject = "HangFire Service Health Check";
                    mailRequest.Body = "service response failed!  <br> request url:" + _taskSettings.ServiceUrl + " <br> Exception: " + ex.Message;
                    await _mailService.SendEmailAsync(mailRequest);
                    #endregion
                }
            }
        }
    }
}


