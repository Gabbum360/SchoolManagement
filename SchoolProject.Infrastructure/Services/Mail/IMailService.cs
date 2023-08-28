using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Services.Mail
{
    public interface IMailService
    {
        void BuildNewStudentRegistrationMessage(string to, string firstname, string lastname, string email, string DateCreated);
        void BuildNewUserRegistrationMessage(string to, string firstname, string adminName, string adminEmail, DateTime activationDate);
    }
}
