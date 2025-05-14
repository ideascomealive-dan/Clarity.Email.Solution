using Clarity.ReliableEmailSender.Entities;
using Clarity.ReliableEmailSender.Services.Interfaces;

namespace Clarity.ReliableEmailSender.Services
{
    public class DefaultEmailLogger : IEmailLogger
    {
        private readonly IEmailLogStore _store;

        public DefaultEmailLogger(IEmailLogStore store)
        {
            _store = store;
        }

        public Task LogAsync(EmailLog log)
        {
            return _store.SaveAsync(log);
        }
    }
}
