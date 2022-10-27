using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProCodeGuide.Samples.Hangfire.Services;

namespace ProCodeGuide.Samples.Hangfire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private bool bayrak=true; // Bunuda diğer yazacağımız katmandan alıcaz. Server çökerse bool değeri true olacak.
        private IEmailService _emailService = null;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpGet]
        public string SendMail()
        {
            BackgroundJob.Enqueue(() => _emailService.SendEmail("Direct Call", DateTime.Now.ToLongTimeString())); //Ateşle ve Unut işi
            BackgroundJob.Schedule(() => _emailService.SendEmail("Delayed Job", DateTime.Now.ToLongTimeString()), TimeSpan.FromSeconds(30));//Gecikmeli iş
            RecurringJob.AddOrUpdate(() => _emailService.SendEmail("Recurring Job", DateTime.Now.ToLongTimeString()), Cron.Minutely);// Yinelenen İş
            if (bayrak == true) //Mesela eğer server çökerse buraya girip mail atma işlemi yapması lazım.
            {
                var jobId = BackgroundJob.Schedule(() => _emailService.SendEmail("Continuation Job 1", DateTime.Now.ToLongTimeString()), TimeSpan.FromSeconds(45));
                BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation Job 2 - Email Reminder - " + DateTime.Now.ToLongTimeString())); // Bu işler, bağlantılı önceki iş başarıyla yürütüldükten hemen sonra yürütülür. (Burayı büyük ihtimalle kullanıcam.) Mesela server çökerse buradaki işe gir.
            }
            return "Email Initiated";
        }
    }
}
