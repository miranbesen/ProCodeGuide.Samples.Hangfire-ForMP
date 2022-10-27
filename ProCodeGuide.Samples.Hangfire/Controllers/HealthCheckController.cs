using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ProCodeGuide.Samples.Hangfire.Services;
using Microsoft.Extensions.Options;
using ProCodeGuide.Samples.Hangfire.Settings;

namespace ProCodeGuide.Samples.Hangfire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        //private bool flag = true; // Bunuda diğer yazacağımız katmandan alıcaz. Server çökerse bool değeri true olacak.
        private IHealthCheckService _healthCheckService = null;
        private readonly TaskSettings _taskSettings;

        public HealthCheckController(IHealthCheckService healthCheckService, IOptions<TaskSettings> taskSettings)
        {
            _healthCheckService = healthCheckService;
            _taskSettings = taskSettings.Value;
        }

        [HttpGet]
        public string HangFireJobSchedule()
        {
            //BackgroundJob.Enqueue(() => _healthCheckService.HealthCheck("Direct Call", DateTime.Now.ToLongTimeString())); //Ateşle ve Unut işi
            //BackgroundJob.Schedule(() => _healthCheckService.HealthCheck("Delayed Job", DateTime.Now.ToLongTimeString()), TimeSpan.FromMinutes(_taskSettings.ScheduleTime));//Gecikmeli iş
            RecurringJob.AddOrUpdate(() => _healthCheckService.HealthCheck("Recurring Job", DateTime.Now.ToLongTimeString()), "*/" + _taskSettings.ScheduleTime + " * * * *");// Yinelenen İş
            //if (flag == true) //Mesela eğer server çökerse buraya girip mail atma işlemi yapması lazım.
            //{
            //    var jobId = BackgroundJob.Schedule(() => _healthCheckService.HealthCheck("Continuation Job 1", DateTime.Now.ToLongTimeString()), TimeSpan.FromSeconds(45));
            //    BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation Job 2 - Email Reminder - " + DateTime.Now.ToLongTimeString())); // Bu işler, bağlantılı önceki iş başarıyla yürütüldükten hemen sonra yürütülür. (Burayı büyük ihtimalle kullanıcam.) Mesela server çökerse buradaki işe gir.
            //}
            return "HangFire Job Schedule";
        }
    }
}
