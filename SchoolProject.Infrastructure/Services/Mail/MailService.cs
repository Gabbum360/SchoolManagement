﻿using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;
using MailKit;
using Microsoft.Extensions.Options;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Services.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BodyPart = ElasticEmail.Model.BodyPart;
using IMailService = SchoolProject.Infrastructure.Services.Mail.IMailService;

namespace SchoolProject.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private MailOptions _mailOptions;

        public MailService(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
        }

        public void BuildNewStudentRegistrationMessage(string to, string firstname, string lastname, string email, string DateCreated)
        {
            var builder = new MailBuilder();
            var mailObject = builder.WithFromEmail(to)
                .WithToEmail(email)
                .WithSubject(_mailOptions.StudentSuccessfulRegistrationMessage)
                .BuildNewStudentRegistrationMessage(firstname, lastname, email, DateCreated)
                .BuildMailDto();
                SendEmail(mailObject);
        }
        public void BuildNewUserRegistrationMessage(string to, string firstname, string adminName, string adminEmail, DateTime activationDate)
        {
            var builder = new MailBuilder();
            var mailObject = builder.WithFromEmail(to)
                .WithToEmail(adminEmail)
                .WithSubject(_mailOptions.UserSuccessfulRegistrationMessage)
                .BuildNewUserRegistrationMessage(firstname, adminName, adminEmail, activationDate)
                .BuildMailDto();
            SendEmail(mailObject);
        }
        /*public void BuildNewStudentRegistrationMessageToAdmin(string adminEmail, string studentFullName, string email, string DateCreated)
        {

        }*/

        private void SendEmail(MailObject dto)
        {
            Configuration config = new Configuration();
            // Configure API key authorization: apikey
            config.ApiKey.Add("X-ElasticEmail-ApiKey", _mailOptions.APIKey);
            var apiInstance = new EmailsApi(config);
            var to = new List<string> { dto.To };
            var recipients = new TransactionalRecipient(to: to);
            EmailTransactionalMessageData emailData = new EmailTransactionalMessageData(recipients: recipients)
            {
                Content = new EmailContent
                {
                    Body = new List<BodyPart>()
                }
            };
            BodyPart htmlBodyPart = new BodyPart
            {
                ContentType = BodyContentType.HTML,
                Charset = "utf-8",
                Content = dto.BodyAmp
            };
            BodyPart plainTextBodyPart = new BodyPart
            {
                ContentType = BodyContentType.PlainText,
                Charset = "utf-8",
                Content = dto.BodyAmp
            };
            emailData.Content.Body.Add(htmlBodyPart);
            emailData.Content.Body.Add(plainTextBodyPart);
            emailData.Content.From = dto.From;
            emailData.Content.Subject = dto.Subject;

            try
            {
                // Send Bulk Emails
                var result = apiInstance.EmailsTransactionalPost(emailData);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling EmailsApi.EmailsPost: " + e.Message);
                Debug.Print("Status Code: " + e);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
