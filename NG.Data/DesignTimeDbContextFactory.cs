using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace NG.Data
{
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<NGDbContext>
    {
        public NGDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NGDbContext>();
            builder.UseSqlServer("Server=.;Database=NG;Trusted_Connection=True;");
            var context = new NGDbContext(builder.Options);
            return context;
        }
    }
}
