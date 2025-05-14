using Clarity.ReliableEmailSender.Entities;

namespace Clarity.ReliableEmailSender.Services.Interfaces
{
    public interface IEmailLogger
    {
        Task LogAsync(EmailLog log);
    }
}
