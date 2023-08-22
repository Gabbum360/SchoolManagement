using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Services.Mail
{
    public interface IMailBuilder
    {
        MailBuilder BuildNewStudentRegistrationMessage(string firstname, string lastname, string email, string DateCreated);
        MailBuilder BuildNewAdminRegistrationMessage(string adminName, string adminEmail, string DateCreated);
        MailBuilder BuildNewStudentRegistrationMessageToAdmin(string adminEmail, string studentFullName, string email, string DateCreated);
    }
}
