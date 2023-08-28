using SchoolProject.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain.Core.Entities
{
    public class RequestBook
    {
        public RequestBook(Guid id, Guid studentId, int classId)
        {
            Id = id;
            StudentId = studentId;
            ClassId = classId;
        }
        private RequestBook()
        {

        }

        public Guid Id { get;set;}
        public Guid StudentId { get; private set;}
        public Student Student { get;set;}
        public int ClassId { get; private set;}
        public LibraryStatus LibraryStatus { get;set;}
        public ApprovalStatus ApprovalStatus { get; set; }
        public SchoolSection SchoolSection { get; set; }
        public Book Book { get; set; }

        public RequestBook RejectRequest()
        {
            this.ApprovalStatus = ApprovalStatus.Rejected;
            return this;
        }
        public RequestBook ApproveRequest()
        {
            this.ApprovalStatus = ApprovalStatus.Approved;
            return this;
        }

        public RequestBook SetStudentId(Guid id)
        {
            this.StudentId = id;
            return this;
        }
        public RequestBook SetClassId(int id)
        {
            this.ClassId = id;
            return this;
        }

        public class Factory
        {
            public static RequestBook Create(Guid id, Guid studentId, int classId)
            {
                return new RequestBook(id, studentId, classId);
            }

            public static RequestBook Create()
            {
                return new RequestBook();
            }
        }

    }
}
