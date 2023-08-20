using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain
{
    public class Teacher
    {
        public Teacher() {}
        public Teacher(Guid id, string firstname, string surname, int age, string sex, string country, int staffCount)
        {
            Id = id;
            FirstName = firstname;
            SurName = surname;
            Age = age;  
            Sex = sex;
            Country = country;
            StaffNo = GenerateStudentNo(staffCount);
        }
        public Guid Id { get; set; }
        public string SurName { get; private set; }
        public string FirstName { get; private set; }
        public int Age { get; private set; }
        public string Sex { get; private set; }
        public string Country { get; private set; }
        public string StaffNo { get; private set; }


        public Teacher SetFirstName(string firstname)
        {
            this.FirstName = firstname;
            return this;
        }
        public Teacher SetSurname(string surname)
        {
            this.SurName = surname;
            return this;
        }
        public Teacher SetAge(int age)
        {
            this.Age = age;
            return this;
        }
        public Teacher SetSex(string sex)
        {
            this.Sex = sex;
            return this;
        }
        public Teacher SetStudentNo(string studentNo)
        {
            this.StaffNo = studentNo;
            return this;
        }
        public Teacher SetCountry(string country)
        {
            this.Country = country;
            return this;
        }
        public static class TeacherFactory
        {
            public static Teacher Create(Guid id, string firstname, string surname, int age, string sex, string country, int staffCount)
            {
                return new Teacher(id, firstname, surname, age, sex, country, staffCount);
            }
            public static Teacher Create()
            {
                return new Teacher();
            }
        }
        private string GenerateStudentNo(int staffCount)
        {

            var studentNo = $"fgc{staffCount + 1}";
            return studentNo;
        }
    }
}
