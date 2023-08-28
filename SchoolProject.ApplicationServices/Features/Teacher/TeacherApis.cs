using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.Core.Domain;
using SchoolProject.Core.Domain.Core.Dto;
using SchoolProject.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Features.Teacher
{
    public class TeacherApis
    {
        private readonly SPSDbContext _context;
        private readonly ILogger<TeacherApis> _logger;
        private readonly IPublisher _publish;
        public TeacherApis(SPSDbContext context, ILogger<TeacherApis> logger, IPublisher publish)
        {
            _context = context;
            _logger = logger;
            _publish = publish;
        }
        //Logics
        public async Task<bool> RemoveFromRecord(Guid id)
        {
            var checkId = _context.Teachers.Where(x => x.Id == id).FirstOrDefault();
            if(checkId == null)
            {
                _logger.LogError("data fetch error or Id don't exist!");
            }
            _context.Remove<Core.Domain.Teacher>(checkId);
            var isDone = _context.SaveChangesAsync();
            if (isDone.IsCompletedSuccessfully)
            {
                _logger.LogInformation("data with: {id} is deleted successfully", id);
            }
            return false;
        }

        public async Task<AddTeacherDto> AddNewTeacher(Guid id, string firstname, string surname, int age, string sex, string country, string staffNo)
        {
            try
            {
                var teacherId = Guid.NewGuid();
                var newTeacher = Core.Domain.Teacher.TeacherFactory.Create(teacherId, firstname, surname, age, sex, country, staffNo).SetSurname(surname).SetFirstName(firstname).SetCountry(country)
                    .SetSex(sex).SetAge(age).SetStaffNo(staffNo);
                if (newTeacher == null)
                {
                    _logger.LogError("please fill in the blank correctly");
                }
                await _context.AddAsync(newTeacher);
                var isSaved = await _context.SaveChangesAsync();
                if (isSaved > 0)
                {
                    //return new AddTeacherDto();
                    _logger.LogInformation("{}: add successfully!", teacherId);
                }
                //send welcome mail
                /*_publish.Publish(new WelcomeEmailMessage()
                {

                })*/
            }
            catch (Exception e)
            {

                _logger.LogError("Error occured", e);
            }
            return new AddTeacherDto();
        }

        public async Task<List<GetTeacherDto>> GetRegisteredTeachers()
        {
            List<GetTeacherDto> teachers = new List<GetTeacherDto>();
            var teachersFromDb = await _context.Teachers?.ToListAsync();
            if (teachersFromDb == null)
            {
                _logger.LogError("Error fetching data");
            }
            foreach (var item in teachersFromDb)
            {
                GetTeacherDto teacher = new()
                {
                    Id = item.Id,
                    SurName = item.SurName,
                    Sex = item.Sex,
                    Age = item.Age,
                    StaffNo = item.StaffNo,
                    FirstName = item.FirstName,
                    Country = item.Country,
                };
            };
            return teachers;
        }
    }
    public record AddTeacherDto
    {
        public Guid Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string StaffNo { get; set; }
        public static explicit operator AddTeacherDto(Core.Domain.Teacher dto)
        {
            return new AddTeacherDto()
            {
                Id = dto.Id,
                SurName = dto.SurName,
                FirstName = dto.FirstName,
                Age = dto.Age,
                Sex = dto.Sex,
                Country = dto.Country,
                StaffNo = dto.StaffNo,
            };
        }
    }
}
