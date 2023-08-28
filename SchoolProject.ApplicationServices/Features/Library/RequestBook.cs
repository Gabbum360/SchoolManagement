using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolProject.ApplicationServices.Features.StudentManagement;
using SchoolProject.Core.Domain;
using SchoolProject.Core.Domain.Core.Entities;
using SchoolProject.Core.Domain.Enums;
using SchoolProject.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Features.Library
{
    public record RequestBook(Guid StudentId, int ClassId, Guid RequestId) : IRequest<bool>;

    public class AssignBookHandler : IRequestHandler<RequestBook, bool>
    {
        private readonly ILogger<AssignBookHandler> _logger;
        private readonly SPSDbContext _context;
        public AssignBookHandler(ILogger<AssignBookHandler> logger, SPSDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(RequestBook request, CancellationToken cancellationToken)
        {
            var confirmStudentInfo = _context.Students.Where(d => d.Id == request.StudentId).Any();
            if(confirmStudentInfo == null)
            {
                _logger.LogError("Student is invalid or dont exist!");
            }
            var requestId = Guid.NewGuid();
            var result = Core.Domain.Core.Entities.RequestBook.Factory.Create(requestId, request.StudentId, request.ClassId).SetStudentId(request.StudentId).SetClassId(request.ClassId);
            await _context.AddAsync(result);
            var isSaved = await _context.SaveChangesAsync();
            if (isSaved > 0) return true;

            return false;
        }
    }

}
