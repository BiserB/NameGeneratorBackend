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

        public DbSet<Name> MaleNames { get; set; }

        public DbSet<NameType> NameTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Name>().HasKey(n => n.Id);
            mb.Entity<Name>().Property(n => n.Id).ValueGeneratedNever();
            mb.Entity<Name>().HasOne(n => n.Type)
                            .WithMany(t => t.Names)
                            .HasForeignKey(n => n.NameTypeId)
                            .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<NameType>().HasKey(nt => nt.Id);
            mb.Entity<NameType>().Property(nt => nt.Id).ValueGeneratedNever();
        }
    }
}
