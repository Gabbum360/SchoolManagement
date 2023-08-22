using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Services
{
    public class MailBuilder : IMailBuilder
    {
        //by default its private modifier.
        string _toEmail;
        string _subject;
        string _fromEmail;
        string _message;


        public MailBuilder WithToEmail(string toEmail)
        {
            _toEmail = toEmail;
            return this;
        }

        public MailBuilder WithFromEmail(string fromEmail)
        {
            _fromEmail = fromEmail;
            return this;
        }

        public MailBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }
        public MailBuilder BuildNewStudentRegistrationMessage(string firstname, string lastname, string email, string DateCreated)
        {
            var rawTemplate = FileHelper.ExtractMailTemplate("page9-loan-approved-by-customer-and-disbursed.html");
            var _message = rawTemplate.Replace("{Student's Lastname}", lastname)
                .Replace("{Student's Email}", email)
                .Replace("{Student FirstName}", firstname)
                .Replace("{Date Registered}", DateCreated);
            return this;
        }
        public MailBuilder BuildNewStudentRegistrationMessageToAdmin(string adminEmail, string studentFullName, string email, string DateCreated)
        {
            var rawTemplate = FileHelper.ExtractMailTemplate("page9-loan-approved-by-customer-and-disbursed.html");
            var _message = rawTemplate.Replace("{Admin's Email}", adminEmail)
                .Replace("{Student's Email}", email)
                .Replace("{Student Name}", studentFullName)
                .Replace("{Date Registered}", DateCreated);
            return this;
        }
        public MailBuilder BuildNewAdminRegistrationMessage(string adminName, string adminEmail, string DateCreated)
        {
            var rawTemplate = FileHelper.ExtractMailTemplate("page9-loan-approved-by-customer-and-disbursed.html");
            var _message = rawTemplate
                .Replace("{Admin's Email}", adminEmail)
                .Replace("{Admin's Name}", adminName)
                .Replace("{Date Registered}", DateCreated);
            return this;
        }
        public MailObject BuildMailDto()
        {
            return new MailObject()
            {
                BodyAmp = _message,
                CharSet = "utf-8",
                From = _fromEmail,
                IsTransactional = true,
                To = _toEmail,
                Sender = _fromEmail,
                Subject = _subject
            };
        }

        public static implicit operator MailObject(MailBuilder builder)
        {
            return builder.BuildMailDto();
        }
    }
}
