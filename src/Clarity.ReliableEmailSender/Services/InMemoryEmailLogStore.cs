using Clarity.ReliableEmailSender.Entities;
using Clarity.ReliableEmailSender.Services.Interfaces;
using System.Collections.Concurrent;

namespace Clarity.ReliableEmailSender.Services
{
    public class InMemoryEmailLogStore : IEmailLogStore
    {
        private readonly ConcurrentBag<EmailLog> _logs = new();

        public Task SaveAsync(EmailLog log)
        {
            _logs.Add(log);
            return Task.CompletedTask;
        }

        public Task<List<EmailLog>> GetAllAsync()
        {
            return Task.FromResult(_logs.ToList());
        }
    }
}
