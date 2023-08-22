using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Common
{
    public static class FileHelper
    {
        public static string ExtractMailTemplate(string fileName)
        {
            var mailTemplate = Path.Combine(Directory.GetCurrentDirectory(), $"Email_Templates_Borrow_Ease/{fileName}");
            string[] lines = File.ReadAllLines(mailTemplate);
            var template = StringHelper.ConvertArrayToString(lines);
            return template;
        }
    }
}
