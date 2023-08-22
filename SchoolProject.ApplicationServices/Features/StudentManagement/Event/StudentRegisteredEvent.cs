using MediatR;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Features.StudentManagement.Event
{
    public class StudentRegisteredEvent : INotification
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AdminEmail { get; set; }
        public string DateCreated { get; set; }
    }

    public class StudentRegisteredEventHandler : INotificationHandler<StudentRegisteredEvent>
    {
        private readonly IMailService _mailService ;
        public async Task Handle(StudentRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _mailService.BuildNewStudentRegistrationMessage(notification.Email, notification.FirstName, notification.Lastname, notification.AdminEmail, notification.DateCreated);
        }
    }
}
