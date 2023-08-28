using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Persistence.Context
{
        public class SPSDbContextFactory : IDesignTimeDbContextFactory<SPSDbContext>
        {
            public SPSDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<SPSDbContext>();
                optionsBuilder.UseSqlServer("data source=DESKTOP-HI70H7F\\SQLEXPRESS02;initial catalog=SchoolManagementSystem-RC;MultipleActiveResultSets=true;integrated security=true;");

                return new SPSDbContext(optionsBuilder.Options);
            }
        }
}
