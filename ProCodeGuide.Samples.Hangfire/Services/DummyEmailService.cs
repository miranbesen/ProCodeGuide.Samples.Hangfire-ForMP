namespace ProCodeGuide.Samples.Hangfire.Services
{
    public class DummyEmailService : IEmailService
    {
        public void SendEmail(string backGroundJobType, string startTime)
        {
            Console.WriteLine(backGroundJobType + " - " + startTime + " - Email Sent - " + DateTime.Now.ToLongTimeString());
        }
    }
}
    

