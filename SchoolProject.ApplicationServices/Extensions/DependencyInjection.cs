using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Core.Domain;
using SchoolProject.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddDbContext<SPSDbContext>(d => d.UseSql(configuration.GetConnectionString("SPConnection")));
            //for identity
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores <SPSDbContext>()
                .AddDefaultTokenProviders();*/
            return services;
        }
    }
}