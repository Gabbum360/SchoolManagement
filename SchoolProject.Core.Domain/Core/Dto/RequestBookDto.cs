using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain.Core.Dto
{
    public record RequestBookDto
    {
        public Guid StudentId { get; set; }
        public int ClassId { get; set; }
        public Guid Id { get; set; }

        public static explicit operator RequestBookDto(Entities.RequestBook model)
        {
            return new RequestBookDto()
            {
                StudentId = model.StudentId,
                ClassId = model.ClassId,
                Id = model.Id,
            };
        }
    }
}
