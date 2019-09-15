using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NG.Data;
using NG.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NG.App.Infrastructure
{
    public static class InitialSeeder
    {
        public static async void InitialSeed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetService<NGDbContext>();
                var userManager = serviceProvider.GetService<UserManager<NGUser>>();

                dbContext.Database.Migrate();

                await SeedUsers(dbContext, userManager);

                await SeedNames(dbContext);
            }
        }

        private static async Task SeedUsers(NGDbContext dbContext, UserManager<NGUser> userManager)
        {            

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

        private static async Task SeedNames(NGDbContext dbContext)
        {
            if (dbContext.MaleFirstNames.Any())
            {
                return;
            }

            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string basePath = Path.GetFullPath(Path.Combine(directoryPath, @"..\..\..\.."));

            string fullPath = Path.Combine(basePath, @"NG.Files\male.csv");

            string[] lines = File.ReadAllLines(fullPath);

            List<MaleFirstName> names = new List<MaleFirstName>(lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                int id = i + 1;
                var name = new MaleFirstName()
                {
                    Id = id,
                    Name = lines[i]
                };

                names.Add(name);
            }

            dbContext.MaleFirstNames.AddRange(names);

            await dbContext.SaveChangesAsync();
        }
    }
}
