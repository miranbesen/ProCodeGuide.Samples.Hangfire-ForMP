namespace ProCodeGuide.Samples.Hangfire.Model
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }        
    }
}