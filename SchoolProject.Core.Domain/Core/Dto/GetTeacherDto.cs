using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain.Core.Dto
{
    public class GetTeacherDto
    {
        public Guid Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string StaffNo { get; set; }
    }
}
