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

                //SeedTypes(dbContext);

                //SeedFirstNames(dbContext);

                //SeedLastNames(dbContext);                
            }
        }

        private static async Task SeedUsers(NGDbContext dbContext, UserManager<NGUser> userManager)
        {
            if (!dbContext.Users.Any())
            {
                NGUser user = new NGUser()
                {
                    UserName = "Admin",
                    Email = "admin@namegenerator.com",
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

        private static void SeedTypes(NGDbContext dbContext)
        {
            dbContext.NameTypes.Add(new NameType() { Id = 1, Type = "male-first" });
            dbContext.NameTypes.Add(new NameType() { Id = 2, Type = "male-last" });
            dbContext.SaveChanges();
        }

        private static void SeedFirstNames(NGDbContext dbContext)
        {
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string basePath = Path.GetFullPath(Path.Combine(directoryPath, @"..\..\..\.."));

            string fullPath = Path.Combine(basePath, @"NG.Files\maleFirstNames.csv");

            string[] lines = File.ReadAllLines(fullPath);

            List<Name> names = new List<Name>(lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                int id = i + 1;
                var name = new Name()
                {
                    Id = id,
                    Record = lines[i],
                    NameTypeId = 1
                };

                names.Add(name);
            }

            dbContext.MaleNames.AddRange(names);

            dbContext.SaveChanges();
        }                

        private static void SeedLastNames(NGDbContext dbContext)
        {
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string basePath = Path.GetFullPath(Path.Combine(directoryPath, @"..\..\..\.."));

            string fullPath = Path.Combine(basePath, @"NG.Files\maleLastNames.csv");

            string[] lines = File.ReadAllLines(fullPath);

            List<Name> names = new List<Name>(lines.Length);

            int id = dbContext.MaleNames.Last().Id;

            for (int i = 0; i < lines.Length; i++)
            {
                var name = new Name()
                {
                    Id = ++id,
                    Record = lines[i],
                    NameTypeId = 2
                };

                names.Add(name);
            }

            dbContext.MaleNames.AddRange(names);

            dbContext.SaveChanges();
        }
    }
}
