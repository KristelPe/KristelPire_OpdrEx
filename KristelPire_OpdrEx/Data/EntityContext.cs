using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KristelPire_OpdrEx.Entities;

namespace KristelPire_OpdrEx.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(b => b.Id);
            modelBuilder.Entity<Car>().HasOne(b => b.Owner);
            modelBuilder.Entity<Car>().HasOne(b => b.Type).WithMany(b => b.Cars);
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}