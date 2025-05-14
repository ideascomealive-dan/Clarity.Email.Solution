using Clarity.ReliableEmailSender.Entities;

namespace Clarity.ReliableEmailSender.Services.Interfaces
{
    public interface IEmailLogStore
    {
        Task SaveAsync(EmailLog log);

        Task<List<EmailLog>> GetAllAsync(); // For debugging/testing
    }
}
