using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain
{
    public record AddStudentDto
    {
        public Guid Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public Guid ClassArmId { get; set; }
        public string Country { get; set; }
        public string StudentNo { get; set; }

        public static explicit operator AddStudentDto(Student model)
        {
            return new AddStudentDto()
            {
                Id = model.Id,
                SurName = model.SurName,
                FirstName = model.FirstName,
                Age = model.Age,
                Sex = model.Sex,
                ClassArmId = model.ClassArmId,
                Country = model.Country,
                StudentNo = model.StudentNo,
            };
        }
    }
    public record GetStudentDto
    {
        public Guid Id { get; set; }
        public string SurName { get; set; }
        //public string FirstName { get; set; }
        //public int Age { get; set; }
        public string Sex { get; set; }
        //public Guid ClassArmId { get; set; }
        //public string Country { get; set; }
        public string StudentNo { get; set; }

        public static explicit operator GetStudentDto(Student model)
        {
            return new GetStudentDto()
            {
                Id = model.Id,
                SurName = model.SurName,
                //FirstName = model.FirstName,
                //Age = model.Age,
                Sex = model.Sex,
               //ClassArmId = model.ClassArmId,
                //Country = model.Country,
                StudentNo = model.StudentNo,
            };
        }
    }
}
