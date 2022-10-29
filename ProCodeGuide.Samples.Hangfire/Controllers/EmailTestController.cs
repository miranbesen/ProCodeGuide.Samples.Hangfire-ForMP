using Microsoft.AspNetCore.Mvc;
using ProCodeGuide.Samples.Hangfire.Model;
using ProCodeGuide.Samples.Hangfire.Services;

namespace ProCodeGuide.Samples.Hangfire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        private readonly IMailService mailService;
        public EmailTestController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
