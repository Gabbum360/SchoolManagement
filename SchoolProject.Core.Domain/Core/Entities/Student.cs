using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain
{
    public class Student
    {
        private Student() { }
        public Student(Guid id, string firstname, string surname, int age, string sex, string country, int studentCount)
        {
            //validation on OOP...
            Id = id;
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstName can not be null");
            if (firstname.Length < 5)
                throw new ArgumentException("first name must be greater than 5");
            FirstName = firstname;

            if (string.IsNullOrEmpty(surname))
                throw new ArgumentNullException("");
            if (surname.Length < 7)
                throw new ArgumentException("must be greater than 7");
            SurName = surname;

            if (age <= 5)
                throw new ArgumentNullException("age must be 6years and above");
            Age = age;

            if (string.IsNullOrEmpty(sex))
                throw new ArgumentNullException("Sex can not be empty");
            Sex = sex;

            if (country == null)
                throw new ArgumentNullException("can not be null");
            Country = country;

            StudentNo = GenerateStudentNo(studentCount);


        }
        public Guid Id { get; set; }
        public string SurName { get; private set; }
        public string FirstName { get; private set; }
        public int Age { get; private set; }
        public string Sex { get; private set; }
        public Guid ClassArmId { get; set; }
        public string Country { get; private set; }
        public string StudentNo { get; private set; }
        public List<Book> GetBooks { get; set; } = new List<Book>();


        public Student SetFirstName(string firstname)
        {
            this.FirstName = firstname;
            return this;
        }
        public Student SetSurname(string surname)
        {
            this.SurName = surname;
            return this;
        }
        public Student SetAge(int age)
        {
            this.Age = age;
            return this;
        }
        public Student SetSex(string sex)
        {
            this.Sex = sex;
            return this;
        }
        public Student SetStudentNo(string studentNo)
        {
            this.StudentNo = studentNo;
            return this;
        }
        public Student SetCountry(string country)
        {
            this.Country = country;
            return this;
        }
        public class StudentFactory
        {
            public static Student Create(Guid id, string firstname, string surname, int age, string sex, string country, int studentCount)
            {
                return new Student(id, firstname, surname, age, sex, country, studentCount);
            }
            public static Student Create()
            {
                return new Student();
            }
        }
        private string GenerateStudentNo(int studentCount)
        {

            var studentNo = $"fgc{studentCount + 1}";
            return studentNo;
        }
    }
}
