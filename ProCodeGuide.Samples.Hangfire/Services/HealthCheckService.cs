using Microsoft.Extensions.Options;
using ProCodeGuide.Samples.Hangfire.Model;
using ProCodeGuide.Samples.Hangfire.Model.Context;
using ProCodeGuide.Samples.Hangfire.Settings;

namespace ProCodeGuide.Samples.Hangfire.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly ILogger<HealthCheckService> _logger;
        private readonly IMailService _mailService;
      
        public HealthCheckService(ProCodeGuideSamplesHangfireContext dbContext, ILogger<HealthCheckService> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="backGroundJobType"></param>
        /// <param name="startTime"></param>
        /// <param name="serviceUrl"></param>
        /// <param name="toMail"></param>
        /// <returns></returns>
        public async Task HealthCheck(string backGroundJobType, string startTime, string serviceUrl, string toMail)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    _logger.LogInformation("service check");
                    //send request 
                    HttpResponseMessage response = await client.GetAsync(serviceUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("service response success request url:" + serviceUrl);
                    }
                    else
                    {
                        #region Request Email Send

                        MailRequest mailRequest = new MailRequest();
                        mailRequest.ToEmail = toMail;//"devops@mucitpanda.com";
                        mailRequest.Subject = "HangFire Service Health Check";
                        mailRequest.Body = "service response failed!  <br>  Statu Code: " + response.StatusCode + " <br> Content: " + response.Content;
                        await _mailService.SendEmailAsync(mailRequest);
                        #endregion
                        _logger.LogInformation("service response failed! request url:" + serviceUrl + "  Statu Code: " + response.StatusCode + " Content: " + response.Content);
                    }
                    await Task.Delay(1000);

                }
                catch (Exception ex)
                {
                    _logger.LogError("Service HealthCheck request url:" + serviceUrl + " Exception: " + ex.Message);

                    #region Email Request

                    MailRequest mailRequest = new MailRequest();
                    mailRequest.ToEmail = toMail;
                    mailRequest.Subject = "HangFire Service Health Check";
                    mailRequest.Body = "service response failed!  <br> request url:" + serviceUrl + " <br> Exception: " + ex.Message;
                    await _mailService.SendEmailAsync(mailRequest);
                    #endregion
                }

            }
        }
    }
}


