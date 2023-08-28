using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Common
{
    public class MailOptions
    {
        public string StudentSuccessfulRegistrationMessage { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string APIKey { get; set; }
        public string BaseAddress { get; set; }
        public string UserSuccessfulRegistrationMessage { get; set; }

    }
}
