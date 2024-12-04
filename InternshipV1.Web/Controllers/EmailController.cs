using InternshipV1.Service.Helper;
using InternshipV1.Service.NewsLetterService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternshipV1.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly INewsLetterService _newsLetterService;

        public EmailController(IEmailService emailService,
                               INewsLetterService newsLetterService)
        {
            _emailService = emailService;
            _newsLetterService = newsLetterService;
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(string reciever, string subject, string body)
        {
            await _emailService.SendEmail(reciever, subject, body);
            return Ok("Email Has Been Sent");
        }

        [HttpPost]
        public async Task<ActionResult> SendNewsLetter( string subject, string body)
        {
            await _newsLetterService.SendNewsletter( subject, body);
            return Ok("Emails Has Been Sent");
        }
    }
}
