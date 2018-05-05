using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TwitterBackup.Data.Context;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;
using TwitterBackup.Web.Services;

namespace TwitterBackup.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TwitterBackupDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TwitterBackupDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddAutoMapper(cfg => cfg.ValidateInlineMaps = false);

            services.AddTransient<IJsonDeserializer, JsonDeserializer>();
            services.AddTransient<ITwitterAPIClient, TwitterApiClient>(serviceProvider =>
            {
                return new TwitterApiClient(Environment.GetEnvironmentVariable("TwitterConsumerKey"),
                    Environment.GetEnvironmentVariable("TwitterConsumerKeySecret"),
                    Environment.GetEnvironmentVariable("TwitterAccessToken"),
                    Environment.GetEnvironmentVariable("TwitterAccessTokenSecret"));
            });
            services.AddTransient<ITwitterAPIService, TwitterApiService>();
            services.AddTransient<ITweetService, TweetService>();
            services.AddTransient<IAutoMapper, AutoMapperWrapper>();
            services.AddTransient<IWorkSaver, WorkSaver>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
