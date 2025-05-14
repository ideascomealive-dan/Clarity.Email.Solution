namespace Clarity.ReliableEmailSender.Entities
{
    public class EmailLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string To { get; set; } = default!;

        public string Subject { get; set; } = default!;

        public string Body { get; set; } = default!;

        public DateTime SentDate { get; set; } = DateTime.UtcNow;

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }

        public int RetryCount { get; set; }
    }
}
