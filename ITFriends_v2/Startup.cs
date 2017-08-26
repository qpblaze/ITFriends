using ITFriends_v2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ITFriends_v2.Models.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ITFriends_v2
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
            services.AddTransient<UploadImageService>();
            services.AddTransient<IEmailSender, MessageSenderService>();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}