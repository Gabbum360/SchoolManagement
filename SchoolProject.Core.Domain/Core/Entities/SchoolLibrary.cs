using SchoolProject.Core.Domain.Core.Entities;
using SchoolProject.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain
{
    public class SchoolLibrary
    {
        public string LibraryName { get; set; }
        public int Id { get; set; }
        public Guid BookId { get; set; }
        public Book ClassBook { get; set; } 
        public Grade ClassId { get; set; }    
        public List<Book> Books { get; set; } = new List<Book>();   
        public LibraryStatus Status { get; set; }
        public Guid StudentId { get; set; }
        public Approvers ApprovalStatus { get; set; }
        public ClassCategories ClassCategory { get; set; }
        public bool IsRejected { get; set; }

        public SchoolLibrary Closed()
        {
            this.Status = LibraryStatus.Closed;
            return this;
        }
        public SchoolLibrary Opened()
        {
            this.Status = LibraryStatus.Opened;
            return this;
        }

        public SchoolLibrary AssignBookToStudent(List<Book> books)
        {
            this.Books = books;
            return this;
        }

        public SchoolLibrary ApproveByAdmin()
        {

            this.ApprovalStatus = Approvers.SchoolAdmin;
            return this;
        }

    }
}
