using SchoolProject.Core.Domain.Core.Dto;
using SchoolProject.Core.Domain.Core.Entities;
using SchoolProject.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Persistence.Context;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Domain.Abstractions;

namespace SchoolProject.ApplicationServices.Features.Library
{
    public class LibraryService : ILibraryService
    {
        private readonly SPSDbContext _dbContext;
        private readonly ILogger<LibraryService> _logger;
        public LibraryService(SPSDbContext dbContext, ILogger<LibraryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<GetBookDto> FetchBooksInLibrary(int classId)
        {
            //fetch books based on the classId.
            var getBooks = _dbContext.Bookss.Where(s => s.ClassId == classId).ToList();
            if (getBooks == null)
            {
                _logger.LogError("something wrong fetching data");
            }
            foreach (var book in getBooks)
            {
                GetBookDto item = new GetBookDto();
                item.Id = book.Id;
                item.Title = book.Title;
                item.Description = book.Description;
                item.Author = book.Author;
            }
            return new GetBookDto();
        }
        //other APIs...
    }
}
