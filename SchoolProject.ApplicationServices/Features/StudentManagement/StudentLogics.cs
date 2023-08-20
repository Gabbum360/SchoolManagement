using Microsoft.Extensions.Logging;
using SchoolProject.Core.Domain;
using SchoolProject.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Features.StudentManagement
{
    public class StudentLogics : IStudent
    {
        private readonly ILogger<StudentLogics> _logger;
        private readonly SPSDbContext _DbContext;
        public StudentLogics(SPSDbContext SMDb, ILogger<StudentLogics> logger)
        {
            _DbContext = SMDb;
            _logger = logger;
        }

        public async Task<AddStudentDto> Register(Guid id, string firstName, string surName, int age, string sex, string country, Guid classarmId)
        {
            _logger.LogInformation("entered the Student Registration Logic");
            try
            {
                var studentId = Guid.NewGuid();
                var totalCountsOfStudents = _DbContext.Students.Count();//this line gets the total count of available students...
                var student = Student.StudentFactory.Create(studentId, firstName, surName, age, sex, country, totalCountsOfStudents).SetCountry(country).SetSurname(surName).SetFirstName(firstName)
                    .SetSex(sex).SetAge(age);
                _DbContext.Add(student);
                await _DbContext.SaveChangesAsync();
                return new AddStudentDto();
                //Send Email
            }
            catch (Exception e)
            {
                _logger.LogError(e, "an error occured");
                throw;
            }
        }
    }
}
