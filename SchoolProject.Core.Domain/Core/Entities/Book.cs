using SchoolProject.Core.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain
{
    public class Book
    {
        public Book(Guid id, string title, string description, string author)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int ClassId { get; set; }


        public class Factory
        {
            public static Book Build(Guid id, string title, string description, string author)
            {
                return new Book(id, title, description, author);
            }
        }
    }
}
