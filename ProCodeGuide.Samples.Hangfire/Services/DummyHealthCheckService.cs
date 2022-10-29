namespace ProCodeGuide.Samples.Hangfire.Services
{
    public class DummyHealthCheckService : IHealthCheckService
    {
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
            Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
        }
    }
}
    

