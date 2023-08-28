using Microsoft.Extensions.Logging;
using SchoolProject.Core.Domain.Core.Entities;
using SchoolProject.Core.Domain;
using SchoolProject.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolProject.ApplicationServices.Features.Library
{
    public class AssignBooksToStudent
    {
        private readonly ILogger<AssignBooksToStudent> _logger;
        private readonly SPSDbContext _DbContext;
        public AssignBooksToStudent(SPSDbContext SMDb, ILogger<AssignBooksToStudent> logger)
        {
            _DbContext = SMDb;
            _logger = logger;
        }
        public async Task<bool> AssignBookToStudent(Guid studentId, int classId, Guid bookId, Guid roleId, LibraryStatus status)
        {
            //var bookId = _DbContext.SchoolLibrary.Where(d => d.BookId == book).Include(classId).FirstOrDefaultAsync();
            //var getApprovalByAdmin = _DbContext.Roles.Where(s => s.Id == roleId).FirstOrDefaultAsync();
            var requestId = Guid.NewGuid();
            var student = _DbContext.Students.Where(v => v.Id == studentId).FirstOrDefaultAsync();
            if (student != null)
            {
                /*var libraryStatus = _DbContext.SchoolLibrary.Where(a => a.Status == status).FirstOrDefault();
                if(libraryStatus )*/
                var book = _DbContext.SchoolLibrary.Where(d => d.BookId == bookId).FirstOrDefaultAsync();
                if (book == null)
                {

                    _logger.LogError("request with: {0}", requestId);

                }

                var request = Core.Domain.Core.Entities.RequestBook.Factory.Create(requestId, studentId, classId).SetStudentId(studentId).SetClassId(classId);

                return false;
            }
            return true;
        }
    }
}
