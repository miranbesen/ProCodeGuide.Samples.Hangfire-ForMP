
namespace ProCodeGuide.Samples.Hangfire.Services
{
    public interface IHealthCheckService
    {
        Task HealthCheck(string backGroundJobType, string startTime);
    
    }
}

