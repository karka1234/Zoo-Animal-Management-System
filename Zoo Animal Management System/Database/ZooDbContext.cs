using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;
using Zoo_Animal_Management_System.Models;

namespace Zoo_Animal_Management_System.Database
{
    public class ZooDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enclosure>()
                .HasMany(e => e.Animals)
                .WithOne(e => e.Enclosure)
                .HasForeignKey(e => e.EnclosureId)
                .IsRequired(false);
        }
    }
}
