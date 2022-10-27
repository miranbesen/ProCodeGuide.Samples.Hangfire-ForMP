namespace ProCodeGuide.Samples.Hangfire.Services
{
    public class DummyHealthCheckService : IHealthCheckService
    {
        public async Task HealthCheck(string backGroundJobType, string startTime)
        {
            Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
        }
    }
}
    

