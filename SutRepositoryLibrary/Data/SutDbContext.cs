using Microsoft.EntityFrameworkCore;
using SutLibrary.Entities;
using SutLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SutLibrary.Data
{
    public class SutDbContext : DbContext
    {
        /// <summary>
        /// The default constructor. This is used in Design-time EF migration script creation
        /// </summary>
        public SutDbContext() { }

        public SutDbContext(DbContextOptions<SutDbContext> options)
            : base(options) { }

        /// <summary>
        /// Set PostgreSql-specific provider in unit tests
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql();
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Compound primary keys can only be declared with .HasKey() method
            modelBuilder.Entity<ComplexEntity>()
                .HasKey(c => new { c.Name, c.Code, c.Version, c.Language });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ComplexEntity> ComplexEntities { get; set; }
        public DbSet<TopLevelEntity> TopLevelEntities { get; set; }
    }
}
