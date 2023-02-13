using Amazon.S3;
using Hidden.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hidden.Data;
using Hidden.Web.Api.StartUp.DependencyInjection;
using Hidden.Web.Core.Services;
using Hidden.Web.Core.Services.VenueService;
using System;
using System.Collections.Generic;
using System.Linq;
using Hidden.Services.Interfaces;

namespace Hidden.Web.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton<IConfigurationRoot>(configuration as IConfigurationRoot);   // IConfigurationRoot
            }

            services.AddSingleton<IConfiguration>(configuration);   // IConfiguration explicitly

            string connString = configuration.GetConnectionString("Default");

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
            // There are a number of different Add* methods you can use. Please verify which one you
            // should be using services.AddScoped<IMyDependency, MyDependency>();

            // services.AddTransient<IOperationTransient, Operation>();

            // services.AddScoped<IOperationScoped, Operation>();

            // services.AddSingleton<IOperationSingleton, Operation>();

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();

            services.AddSingleton<Hidden.Data.Providers.IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );
            services.AddSingleton<IBlogService, BlogService>();

            services.AddSingleton<IBlogAdminService, BlogAdminService>();

            services.AddSingleton<IEventService, EventService>();

            services.AddSingleton<IJobAdminService, JobAdminService>();

            services.AddSingleton<IJobPublicService, JobPublicService>();
            
            services.AddSingleton<IJobTypeService, JobTypeService>();

            services.AddSingleton<INewsletterService, NewsletterService>();

            services.AddSingleton<INewsletterSubscriptionService, NewsletterSubscriptionService>();

            services.AddSingleton<INewsletterTemplatesService, NewsletterTemplatesService>();



            GetAllEntities().ForEach(tt =>
            {
                IConfigureDependencyInjection idi = Activator.CreateInstance(tt) as IConfigureDependencyInjection;

                //This will not error by way of being null. BUT if the code within the method does
                // then we would rather have the error loadly on startup then worry about debuging the issues as it runs
                idi.ConfigureServices(services, configuration);
            });
        }

        public static List<Type> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IConfigureDependencyInjection).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .ToList();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
