using ElasticEmail.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Core.Domain;
using SchoolProject.Data.Persistence.Context;
using SchoolProject.Infrastructure.Common;
using SchoolProject.Infrastructure.Services;
using SchoolProject.Infrastructure.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ApplicationServices.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetConnectionString("SPConnection");
            services.AddDbContext<SPSDbContext>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            //for identity
            /*services.AddIdentityCore<IdentityUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<SPSDbContext>()
                .AddDefaultTokenProviders();*/
            //services.Configure<MailOptions>(configuration.GetSection("MailOptions"));
            services.AddTransient<IMailBuilder, MailBuilder>();
            services.AddTransient<IMailService, MailService>();
            return services;
        }
    }
}