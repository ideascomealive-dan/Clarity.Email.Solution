using Clarity.ReliableEmailSender.Configurations;
using Clarity.ReliableEmailSender.Services;
using Clarity.ReliableEmailSender.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
    .ConfigureServices((context, services) =>
    {
        services.Configure<SmtpSettings>(context.Configuration.GetSection(SmtpSettings.Position));
        services.AddSingleton<IEmailSender, SmtpEmailSender>();
        services.AddSingleton<IEmailLogStore, InMemoryEmailLogStore>();
        services.AddSingleton<IEmailLogger, DefaultEmailLogger>();

    })
    .Build();

var emailSender = host.Services.GetRequiredService<IEmailSender>();

Console.WriteLine("Enter recipient email:");
string to = Console.ReadLine() ?? "";

Console.WriteLine("Enter subject:");
string subject = Console.ReadLine() ?? "Test Subject";

Console.WriteLine("Enter message body:");
string body = Console.ReadLine() ?? "Test Body";

await emailSender.SendEmailAsync(to, subject, body);

Console.WriteLine("Email attempted. Check logs for result.");