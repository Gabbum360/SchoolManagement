using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Domain;
using SchoolProject.Core.Domain.Core.Dto.AuthDtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoolProject.Data.Persistence.Context
{
    public class SPSDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        /*public SPSDbContext()
        {

        }*/
        public SPSDbContext(DbContextOptions<SPSDbContext> options) : base(options) { }
        public Microsoft.EntityFrameworkCore.DbSet<Student> Students { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Teacher> Teachers { get; set; }
        //public DbSet<Class> Classes { get; set; }
        //public DbSet<Arm> Arms { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Book> Bookss { get; set; }
        //public DbSet<ClassArm> ClassArms { get; set; }
        //public DbSet<ClassTeacher> ClassTeachers { get; set; }
        //public DbSet<Subject> Subjects { get; set; }
        //public DbSet<ClassSubject> ClassSubjects { get; set; }
        //public DbSet<StudentClass> StudentClasses { get; set; }
        //public DbSet<LoginUser> LoginUsers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<LoginModel> LoginModels { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<RegisterModel> RegisterModel { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<SchoolLibrary> SchoolLibrary { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SPSDbContext).Assembly);
            base.OnModelCreating(builder);

            base.OnModelCreating(builder);
            builder.Entity<LoginModel>(
                x =>
                {
                    x.HasNoKey();
                    x.ToView("LoginModel");
                    x.Property(v => v.Username).HasColumnName("username");
                });

            /*builder.Entity<LoginUser>(
                x =>
                {
                    x.HasNoKey();
                    x.ToView("LoginUser");
                    x.Property(m => m.UserName).HasColumnName("Username");
                });*/

            builder.Entity<RegisterModel>(
                x =>
                {
                    x.HasNoKey();
                    x.ToView("RegisterModel");
                    x.Property(m => m.Username).HasColumnName("Username");
                    /*x.Property(m => m.Password).HasColumnName("Password");*/
                });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrivistaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }*/
    }
}
