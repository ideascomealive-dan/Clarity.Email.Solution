using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.ReliableEmailSender.Configurations
{
    // Models/SmtpSettings.cs
    public class SmtpSettings
    {
        public const string Position = "Smtp";
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool UseSSL { get; set; }
    }

}
