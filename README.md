# ReliableEmailSender Solution

A modular and production-ready **email sending system** built with C# and .NET. Designed for high-volume applications where users must never be blocked or delayed due to email failures.

This solution supports:
- ✅ Asynchronous, non-blocking email delivery
- ✅ Retry logic (up to 3 times) using Polly
- ✅ Persistent logging (pluggable: In-Memory by default)
- ✅ Reusability via a Class Library (DLL)
- ✅ Testability with xUnit and Moq
- ✅ Integration into Console App, Web API, and Razor frontend

---

## 📁 Projects Overview

| Project Name              | Description                                   |
|---------------------------|-----------------------------------------------|
| `ReliableEmailSender`     | Core DLL. Handles sending emails and logging. |
| `EmailSender.ConsoleApp`  | Simple console interface to trigger emails.   |
| `EmailSender.WebApi`      | ASP.NET Core Web API with `/api/email` POST. |
| `EmailSender.WebClient`   | Razor Pages frontend to send test emails.     |
| `ReliableEmailSender.Tests` | xUnit test project for the core DLL.       |

---

## 🚀 Getting Started

### Prerequisites
- .NET 6+ SDK
- An SMTP provider (e.g., [Mailtrap](https://mailtrap.io/))

### SMTP Configuration (sample)
Update `appsettings.json` in each app:

```json
"Smtp": {
  "Host": "smtp.mailtrap.io",
  "Port": 4045,
  "User": "enter-your-username",
  "Password": "enter-your-password",
  "UseSSL": false
}
