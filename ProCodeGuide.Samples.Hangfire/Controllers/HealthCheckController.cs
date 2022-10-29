using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ProCodeGuide.Samples.Hangfire.Services;
using Microsoft.Extensions.Options;
using ProCodeGuide.Samples.Hangfire.Settings;
using ProCodeGuide.Samples.Hangfire.Model.Context;

namespace ProCodeGuide.Samples.Hangfire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        //private bool flag = true; // Bunuda diğer yazacağımız katmandan alıcaz. Server çökerse bool değeri true olacak.
        private IHealthCheckService _healthCheckService = null;
        //private readonly TaskSettings _taskSettings;
        private readonly ProCodeGuideSamplesHangfireContext _dbContext;

        public HealthCheckController(IHealthCheckService healthCheckService, ProCodeGuideSamplesHangfireContext dbContext)
        {
            _healthCheckService = healthCheckService;
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string HangFireJobSchedule()
        {
            var taskInformation = _dbContext.TaskInformations.ToList();
            foreach (var task in taskInformation)
            {
                //BackgroundJob.Enqueue(() => _healthCheckService.HealthCheck("Direct Call", DateTime.Now.ToLongTimeString())); //Ateşle ve Unut işi
                //BackgroundJob.Schedule(() => _healthCheckService.HealthCheck("Delayed Job", DateTime.Now.ToLongTimeString()), TimeSpan.FromMinutes(_taskSettings.ScheduleTime));//Gecikmeli iş
                RecurringJob.AddOrUpdate("Service Url:"+ task.ServiceUrl, () => _healthCheckService.HealthCheck("Recurring Job", DateTime.Now.ToLongTimeString(), task.ServiceUrl, task.ToMail), "*/" + task.ScheduleTime + " * * * *");// Yinelenen İş                                                                                                                                                                                    //}
            }
            return "HangFire Job Schedule";

        }
    }
}
