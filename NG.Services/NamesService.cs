using NG.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NG.Services
{
    public class NamesService
    {
        private readonly NGDbContext dbContext;

        public NamesService(NGDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<string> FetchUsernames()
        {
            var usernames = this.dbContext.Users.Select(u => u.UserName).ToList();
            
            return usernames;
        }

        public string CreateRandomName()
        {
            int firstNamesCount = dbContext.MaleNames.Count(n => n.NameTypeId == 1);
            int lastNamesCount = dbContext.MaleNames.Count(n => n.NameTypeId == 2);

            Random random = new Random();
            int randomFirst = random.Next(1, firstNamesCount);
            int randomLast = random.Next(firstNamesCount + 1, firstNamesCount + lastNamesCount);

            string firstName = dbContext.MaleNames.First(n => n.Id == randomFirst).Record;
            string lastName = dbContext.MaleNames.First(n => n.Id == randomLast).Record;
            
            return $"{firstName} {lastName}";
        }

        public string[] CreateRandomNames(int count)
        {
            string[] names = new string[count];
            int firstNamesCount = dbContext.MaleNames.Count(n => n.NameTypeId == 1);
            int lastNamesCount = dbContext.MaleNames.Count(n => n.NameTypeId == 2);

            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                int randomFirst = random.Next(1, firstNamesCount);
                int randomLast = random.Next(firstNamesCount + 1, firstNamesCount + lastNamesCount);

                string firstName = dbContext.MaleNames.First(n => n.Id == randomFirst).Record;
                string lastName = dbContext.MaleNames.First(n => n.Id == randomLast).Record;

                names[i] = $"{firstName} {lastName}";
            } 

            return names;
        }
    }
}
