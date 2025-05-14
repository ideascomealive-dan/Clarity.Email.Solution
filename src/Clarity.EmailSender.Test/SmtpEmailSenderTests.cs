using Clarity.ReliableEmailSender.Configurations;
using Clarity.ReliableEmailSender.Entities;
using Clarity.ReliableEmailSender.Services;
using Clarity.ReliableEmailSender.Services.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using System.Net.Mail;

namespace Clarity.EmailSender.Test
{
    public class SmtpEmailSenderTests
    {
        private readonly Mock<IEmailLogger> _mockLogger;
        private readonly SmtpSettings _smtpSettings;

        public SmtpEmailSenderTests()
        {
            _mockLogger = new Mock<IEmailLogger>();
            _smtpSettings = new SmtpSettings
            {
                Host = "localhost",
                Port = 25,
                User = "test@faker.com",
                Password = "password",
                UseSSL = false
            };
        }

        [Fact]
        public async Task SendEmailAsync_Successful_Send_Should_Log_Once()
        {
            var options = Options.Create(_smtpSettings);
            var sender = new FakeSuccessfulEmailSender(options, _mockLogger.Object);

            await sender.SendEmailAsync("recipient@faker.com", "Test Subject", "Test Body");

            _mockLogger.Verify(l => l.LogAsync(It.Is<EmailLog>(log =>
                log.To == "recipient@faker.com" &&
                log.Subject == "Test Subject" &&
                log.IsSuccess &&
                log.RetryCount <= 3
            )), Times.Once);
        }


        [Fact]
        public async Task SendEmailAsync_Fails_And_Retries_Then_Logs_Failure()
        {
            var options = Options.Create(_smtpSettings);
            var failingSender = new FailingEmailSender(options, _mockLogger.Object);

            EmailLog? capturedLog = null;
            _mockLogger
                .Setup(l => l.LogAsync(It.IsAny<EmailLog>()))
                .Callback<EmailLog>(log => capturedLog = log)
                .Returns(Task.CompletedTask);

            // Act
            await failingSender.SendEmailAsync("recipient@faker.com", "Fail Subject", "Fail Body");

            _mockLogger.Verify(l => l.LogAsync(It.IsAny<EmailLog>()), Times.Once);
            Assert.NotNull(capturedLog);
            Assert.Equal("recipient@faker.com", capturedLog!.To);
            Assert.False(capturedLog.IsSuccess);
            Assert.Equal(4, capturedLog.RetryCount); // 1 initial + 3 retries
            Assert.Contains("Simulated failure", capturedLog.ErrorMessage);
        }


        // Helper class to override Send behavior and throw
        private class FailingEmailSender : SmtpEmailSender
        {
            public FailingEmailSender(IOptions<SmtpSettings> options, IEmailLogger logger)
                : base(options, logger) { }

            protected override async Task SendViaSmtpAsync(string to, string subject, string body)
            {
                await Task.Delay(50);
                throw new SmtpException("Simulated failure");
            }
        }
    }
}