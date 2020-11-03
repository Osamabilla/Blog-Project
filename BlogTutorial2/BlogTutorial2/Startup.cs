using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogTutorial2.Data;
using BlogTutorial2.Data.FileManager;
using BlogTutorial2.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogTutorial2
{
    public class Startup
    {


        //Setting up Database here, not quite sure how it works
        //Apparently, webbuilder in Program.cs looks for appsettings.json in its build, and if it finds the file it loads this in
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //Make the database repository interface available, vett ikje ka Addtransient gjørr heilt
            services.AddTransient<IRepository, Repository>();

            //Make our program aware of the file manager so it can use it
            services.AddTransient<IFileManager, FileManager>();

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                //.AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //Override the redirect url
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            //Adding the database and passing in options, need to go to AppDbContext and receive the options
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config["DefaultConnection"]));

            //MvcOptions.EnableEndpointRouting = false;
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();


        }
    }
}
