using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Core.Domain;
using SchoolProject.Core.Domain.Core.Entities;
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

        public async Task<GetStudentDto> GetStudents()
        {
            try
            {
                List<GetStudentDto> ListOfStudents = new List<GetStudentDto>();
                var studentFromDb = _DbContext.Students.ToList();
                foreach (var record in studentFromDb)
                {
                    GetStudentDto std = new GetStudentDto();
                    std.Id = record.Id;
                    std.SurName = record.SurName;
                    std.StudentNo = record.StudentNo;
                    std.Sex = record.Sex;
                    return std;
                }
                //return new GetStudentDto();
            }
            catch (Exception)
            {
                _logger.LogError("an error occured while fetching from database");
            }
            return new GetStudentDto();
        }

        public async Task<bool> AssignBookToStudent(Guid studentId, string classId, Guid bookId, Guid roleId, LibraryStatus status)
        {
            //var bookId = _DbContext.SchoolLibrary.Where(d => d.BookId == book).Include(classId).FirstOrDefaultAsync();
            //var getApprovalByAdmin = _DbContext.Roles.Where(s => s.Id == roleId).FirstOrDefaultAsync();
            var student = _DbContext.Students.Where(v => v.Id == studentId).FirstOrDefaultAsync();
            if (student != null)
            {
                /*var libraryStatus = _DbContext.SchoolLibrary.Where(a => a.Status == status).FirstOrDefault();
                if(libraryStatus )*/
                var book = _DbContext.SchoolLibrary.Where(d => d.BookId == bookId).Include(classId).FirstOrDefaultAsync();
                if (book != null)
                {
                    var library = new SchoolLibrary();
                    var getApprovalByAdmin = library.ApproveByAdmin();
                    //
                }
                return false;
            }
            return true;
        }
    }
}
