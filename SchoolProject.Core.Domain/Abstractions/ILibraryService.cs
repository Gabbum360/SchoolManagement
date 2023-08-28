using SchoolProject.Core.Domain.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Domain.Abstractions
{
    public interface ILibraryService
    {
        Task<GetBookDto> FetchBooksInLibrary(int classId);
    }
}
