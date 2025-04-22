using Azure.Communication.Email;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace EmailConmunicationServicePOCApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IConfiguration _config;

        public EmailController(ILogger<EmailController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Email email)
        {
            //Need input your connection string in appsettings.json
            var emailClient = new EmailClient(_config.GetConnectionString("EmailService"));
            var emailRecipients = new EmailRecipients(email.Recipients.Select(r => new EmailAddress(r)));
            var emailContent = new EmailContent(email.Subject)
            {
                Html = email.Body
            };

            var emailSendOperation = await emailClient.SendAsync(
                WaitUntil.Completed,
                new EmailMessage(
                    "DoNotReply@62bd4d94-ac36-403d-8b1d-d1399285c29e.azurecomm.net",
                    recipients: emailRecipients,
                    content: emailContent)
            );
            return new JsonResult(new
            {
                Result = "Success"
            });
        }
    }
}
