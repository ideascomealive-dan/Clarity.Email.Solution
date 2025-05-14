using Clarity.ReliableEmailSender.Entities;
using Clarity.ReliableEmailSender.Services.Interfaces;

namespace Clarity.ReliableEmailSender.Services
{
    public class ConsoleEmailLogger : IEmailLogger
    {
        public Task LogAsync(EmailLog log)
        {
            Console.WriteLine($"EmailLog => To: {log.To}, Success: {log.IsSuccess}, Tries: {log.RetryCount}, Error: {log.ErrorMessage}");
            return Task.CompletedTask;
        }
    }

}
