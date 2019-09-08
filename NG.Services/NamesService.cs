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
            var userNames = this.dbContext.Users.Select(u => u.UserName).ToList();

            return userNames;
        }
    }
}
