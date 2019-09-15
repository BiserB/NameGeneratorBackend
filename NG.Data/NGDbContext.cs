using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NG.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NG.Data
{
    public class NGDbContext: IdentityDbContext<NGUser>
    {

        public NGDbContext(DbContextOptions<NGDbContext> options)
            : base(options)
        {

        }

        public DbSet<MaleFirstName> MaleFirstNames { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<MaleFirstName>().Property(mfn => mfn.Id).ValueGeneratedNever();
        }
    }
}
