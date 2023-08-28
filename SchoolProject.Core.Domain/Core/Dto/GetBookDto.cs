using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain.Core.Dto
{
    public record GetBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public static explicit operator GetBookDto(Book model)
        {
            return new GetBookDto()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Author = model.Author,
            };
        }
    }
}
