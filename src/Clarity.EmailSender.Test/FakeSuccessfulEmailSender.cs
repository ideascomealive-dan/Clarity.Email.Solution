using Clarity.ReliableEmailSender.Configurations;
using Clarity.ReliableEmailSender.Services;
using Clarity.ReliableEmailSender.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Clarity.EmailSender.Test
{
    internal class FakeSuccessfulEmailSender : SmtpEmailSender
    {
        public FakeSuccessfulEmailSender(IOptions<SmtpSettings> options, IEmailLogger logger)
            : base(options, logger) { }

        protected override Task SendViaSmtpAsync(string to, string subject, string body)
        {
            // Simulate successful sending without SMTP connection
            return Task.CompletedTask;
        }
    }

}
