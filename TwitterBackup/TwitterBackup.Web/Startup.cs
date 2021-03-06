﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReTwitter.Services.Data.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Context;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;
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
				options.Password.RequireLowercase = false;
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
			services.AddTransient<IUserTweetService, UserTweetService>();
			services.AddTransient<IUserTweeterService, UserTweeterService>();
			services.AddTransient<IAutoMapper, AutoMapperWrapper>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddTransient<IAdminUserService, AdminUserService>();
			services.AddTransient<IStatisticsService, StatisticsService>();
			services.AddTransient<IStoredTweetsStatisticsService, StoredTweetsStatisticsService>();
			services.AddTransient<IFavouriteTweetersStatisticsService, FavouriteTweetersStatisticsService>();
			services.AddTransient<ITweeterService, TweeterService>();
			services.AddTransient<ICascadeDeleteEntityService, CascadeDeleteEntityService>();

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

			Seed(app.ApplicationServices).Wait();
			
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		private async Task Seed(IServiceProvider serviceProvider)
		{ 
			using (var serviceScope = serviceProvider.CreateScope())
			{
				var context = (TwitterBackupDbContext)serviceScope.ServiceProvider.GetService(typeof(TwitterBackupDbContext));

				if (!context.Users.Any())
				{
					var userManager = (UserManager<User>)serviceScope.ServiceProvider.GetService(typeof(UserManager<User>));
					var roleManager = (RoleManager<IdentityRole>)serviceScope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

					Task.Run(async () =>
					{
						//var adminUserName = "admin@admin.com";

						var roleExists = await roleManager.RoleExistsAsync("Administrator");

						if (!roleExists)
						{
							var identityRole = new IdentityRole("Administrator");

							await roleManager.CreateAsync(identityRole);
						}

						var adminEmail = "admin@admin.com";

						var adminUser = await userManager.FindByEmailAsync(adminEmail);

						if (adminUser == null)
						{
							adminUser = new User
							{
								Email = adminEmail,
								UserName = adminEmail
                            };

							await userManager.CreateAsync(adminUser, "mor3c0mpl3xp4$$w0rd");

							await userManager.AddToRoleAsync(adminUser, "Administrator");
						}
					})
					.Wait();
				}

				//if (!context.Tweeters.Any())
				//{
				//	var twApi = (ITwitterAPIService)serviceScope.ServiceProvider.GetService(typeof(ITwitterAPIService));
				//	var tweets = await twApi.GetTweets("realDonaldTrump");
				//	var twSer = (ITweetService)serviceScope.ServiceProvider.GetService(typeof(ITweetService));
				//	var userTweetService = (IUserTweetService)serviceScope.ServiceProvider.GetService(typeof(IUserTweetService));
				//	var firstUserId = context.Users.First().Id;
				//	foreach (var tweet in tweets)
				//	{
				//		await twSer.Add(tweet);
				//		await userTweetService.AddTweetToUserFavouriteCollection(firstUserId, tweet);
				//	}
				//}

				context.SaveChanges();
			}
		}
	}
}
