using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogTutorial2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogTutorial2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Here we build the application
            var host = CreateHostBuilder(args).Build();
            try
            {

                //Not quite sure what this does
                var scope = host.Services.CreateScope();

                //Here we fetch the database, the UserManager to manage the users, and the RoleManager to manage what role the users have
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //Makes sure latest migrations are added
                ctx.Database.EnsureCreated();

                //Create an admin role
                var adminRole = new IdentityRole("Admin");

                if (!ctx.Roles.Any())
                {
                    //Create a role in async way
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    //Create an admin
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };
                    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    //add role to user
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
