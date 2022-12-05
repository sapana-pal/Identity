using Identity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();


            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {
                    var context = services.GetRequiredService<IdentityDbContext>();
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var userManagement = services.GetRequiredService<UserManager<IdentityUser>>();
                    // await ContextSeed.SeedRolesAsync(userManager, roleManager);
                    logger.LogInformation("Finished Seeding Default Data");
                    logger.LogInformation("Application Starting");
                    await context.Database.MigrateAsync();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!await roleManager.RoleExistsAsync(roleName: "Admin"))
                    {
                        var adminRole = new IdentityRole(roleName: "Admin");
                        await roleManager.CreateAsync(adminRole);
                    }
                    if (!await roleManager.RoleExistsAsync(roleName: "SuperAdmin"))
                    {
                        var SuperAdminRole = new IdentityRole(roleName: "SuperAdmin");
                        await roleManager.CreateAsync(SuperAdminRole);
                    }

                }
                catch (Exception ex)
                {

                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
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
