using Clarity.EmailSender.API.Models;
using Clarity.ReliableEmailSender.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clarity.EmailSender.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            try
            {
                await _emailSender.SendEmailAsync(request.To, request.Subject, request.Body);
                return Ok("Email send attempt initiated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sending email: {ex.Message}");
            }
        }
    }
}
