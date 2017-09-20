using ITFriends.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ITFriends.Web.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ITFriends.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ITFriends.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                                    .SetBasePath(env.ContentRootPath)
                                    .AddJsonFile("appsettings.json")
                                    .AddJsonFile("secrets.json")
                                    .AddUserSecrets<Startup>()
                                    .AddEnvironmentVariables()
                                    .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Add the secrets
            services.AddOptions();
            services.Configure<AppSecrets>(Configuration);
            services.Configure<AppSecrets>(secrets =>
            {
                // Cloudinary secrets
                secrets.Cloudinary.CloudName = Configuration["Cloudinary:cloud_name"];
                secrets.Cloudinary.ApiKey = Configuration["Cloudinary:api_key"];
                secrets.Cloudinary.ApiKey = Configuration["Cloudinary:api_secret"];

                // SendGrid secrets
                secrets.SendGrid.ApiKey = Configuration["SendGrid:api_key"];
            });

            // Add the data context
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContextPool<ITFriendsDataContext>(
                options => options.UseSqlServer(connectionString));
            
            // Add custom services
            services.AddTransient<UploadFileReository>();
            services.AddTransient<IEmailSenderRepository, MessageSenderService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add auth
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/sign-in";
                options.LogoutPath = "/sign-out";
                options.AccessDeniedPath = "/sign-in";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });

            // Add the mvc framework
            services.AddMvc();

            // Add auto mapper
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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