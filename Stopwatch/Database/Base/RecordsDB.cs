using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Stopwatch.Database;

namespace Stopwatch.Database.Base
{
    internal class RecordsDB : DbContext
    {
        public DbSet<Records>? Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Records.db");

            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Records>();
        }
    }
}
