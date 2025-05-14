using Clarity.ReliableEmailSender.Configurations;
using Clarity.ReliableEmailSender.Entities;
using Clarity.ReliableEmailSender.Services.Interfaces;
using Microsoft.Extensions.Options;
using Polly;
using System.Net;
using System.Net.Mail;

namespace Clarity.ReliableEmailSender.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _settings;
        private readonly IEmailLogger _logger;

        public SmtpEmailSender(IOptions<SmtpSettings> options, IEmailLogger logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        protected virtual async Task SendViaSmtpAsync(string to, string subject, string body)
        {
            using var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.User, _settings.Password),
                EnableSsl = _settings.UseSSL
            };

            var message = new MailMessage(_settings.User, to, subject, body);
            await smtp.SendMailAsync(message);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            int attempts = 0;
            bool success = false;
            string? error = null;

            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2));

            try
            {
                await retryPolicy.ExecuteAsync(async () =>
                {
                    attempts++;
                    await SendViaSmtpAsync(to, subject, body);
                    success = true;
                });
            }
            catch (Exception ex)
            {
                error = ex.Message;
                success = false;
            }
            finally
            {
                await _logger.LogAsync(new EmailLog
                {
                    To = to,
                    Subject = subject,
                    Body = body,
                    SentDate = DateTime.UtcNow,
                    RetryCount = attempts,
                    IsSuccess = success,
                    ErrorMessage = error
                });
            }
        }
    }
}
