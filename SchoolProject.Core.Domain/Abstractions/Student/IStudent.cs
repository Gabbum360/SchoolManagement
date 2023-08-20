using SchoolProject.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain
{
    public interface IStudent
    {
        Task<AddStudentDto> Register(Guid id, string firstName, string surName, int age, string sex, string country, Guid classarmId);
    }
}
