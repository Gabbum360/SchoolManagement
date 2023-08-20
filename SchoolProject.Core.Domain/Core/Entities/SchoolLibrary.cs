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
        public List<Book> Books { get; set; } = new List<Book>();   
        public string Status { get; set; } = "Opened";

        /*public SchoolLibrary Closed()
        {
            Status = LibraryStatus.Opened;
            return isClosed
        }*/

    }
}
