








namespace ProCodeGuide.Samples.Hangfire.Services
{
    public interface IEmailService
    {
        void SendEmail(string backGroundJobType, string startTime);
    
    }
}

