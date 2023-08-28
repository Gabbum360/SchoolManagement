using MailKit;
using MediatR;
using SchoolProject.Infrastructure.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMailService = SchoolProject.Infrastructure.Services.Mail.IMailService;

namespace SchoolProject.ApplicationServices.Features.AuthenticationManagement.Event
{
    public class UserRegistratedEvent : INotification
    {
        public string Firstname { get; set; }
        public string To { get; set; }
        public DateTime ActivationDate { get; set; }
        public string AdminEmail { get; set; }
        public string AdminName { get; set; }
    }

    public class UserRegistratedEventHandler : INotificationHandler<UserRegistratedEvent>
    {
        private readonly IMailService _mailService;
        public UserRegistratedEventHandler(IMailService mailService)
        {
            _mailService = mailService;
        }
        public async Task Handle(UserRegistratedEvent notification, CancellationToken cancellationToken)
        {
            _mailService.BuildNewUserRegistrationMessage(notification.To, notification.Firstname, notification.AdminName, notification.AdminEmail, notification.ActivationDate);
        }
    }
}
