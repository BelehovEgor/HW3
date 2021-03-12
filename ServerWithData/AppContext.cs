using Microsoft.EntityFrameworkCore;
using ServerWithData.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerWithData
{
    public class AppContext : DbContext
    {
        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbBuilding> Buildings { get; set; }
        public DbSet<DbPhone> Phones { get; set; }
        public DbSet<DbLinkBuildingUser> Links { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\sqlexpress; Database=TestDB; Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
        }
    }
}
