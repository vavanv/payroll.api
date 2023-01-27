using System;

using Microsoft.EntityFrameworkCore;

using Payroll2.Api.DataAccess.PayrollEntities;

namespace Payroll2.Api.DataAccess.PayrollDbContext
{
    public class PayrollDbContext : DbContext
    {
        private readonly string _connectionStringKey;

        public PayrollDbContext(string connectionString)
        {
            _connectionStringKey = connectionString;
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<MasterList> MasterList { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(_connectionStringKey);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new MasterListConfig());
            modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new AddressConfig());
        }
    }
}