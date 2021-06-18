﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Data
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpServerPort { get; set; }
        public Boolean EnableSsl { get; set; }
        public string EmailDisplayName { get; set; }
        public string SenderName { get; set; }

        public EmailSettings()
        {

        }

        public EmailSettings(string smtpServer, string smtpUsername, string smtpPassword, int smtpServerPort)
        {
            this.SmtpServer = smtpServer;
            this.SmtpUsername = smtpUsername;
            this.SmtpPassword = smtpPassword;
            this.SmtpServerPort = smtpServerPort;
        }
    }
}
