using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NG.Data;
using NG.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NG.App.Infrastructure
{
    public class InitialSeeder
    {
        public static async void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dbContext = serviceProvider.GetService<NGDbContext>();

                var userManager = serviceProvider.GetService<UserManager<NGUser>>();

                dbContext.Database.EnsureCreated();

                if (!dbContext.Users.Any())
                {
                    NGUser user = new NGUser()
                    {
                        UserName = "Pesho",
                        Email = "demo@mail.bg",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(user, "PASSword@123");

                    if (result.Succeeded)
                    {
                        Console.WriteLine($"result = {result.Succeeded}");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Errors:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"* {error.Description}");
                        }
                        Console.ResetColor();
                    }
                    
                }
            }
        }
    }
}
