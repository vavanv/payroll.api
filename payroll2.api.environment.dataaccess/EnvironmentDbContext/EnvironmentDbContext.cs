using System;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.Environment.DataAccess.EnvironmentEntities;

namespace Payroll2.Api.Environment.DataAccess.EnvironmentDbContext
{
    public class EnvironmentDbContext : DbContext
    {
        private readonly string _connectionStringKey;

        public EnvironmentDbContext()
        {
        }

        public EnvironmentDbContext(string connectionString)
        {
            _connectionStringKey = connectionString;
        }

        public EnvironmentDbContext(DbContextOptions<EnvironmentDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Systems> Systems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(_connectionStringKey);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new SystemsConfig());
        }
    }
}