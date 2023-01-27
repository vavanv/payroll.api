using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll2.Api.DataAccess.Core.Common;

namespace Payroll2.Api.DataAccess.PayrollEntities
{
    public class Employee : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; } = null;
        public DateTime BirthDate { get; set; }
        public string Sin { get; set; }
        public string Email { get; set; } = null;
        public DateTime DateOfHire { get; set; }
        public int DepartmentId { get; set; }
        public int WageTypeId { get; set; }
        public int VacationPolicyId { get; set; }
        public decimal? VacationRate { get; set; }
        public int AddressId { get; set; }
        public int PositionId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
        public virtual MasterList Department { get; set; }
        public virtual MasterList WageType { get; set; }
        public virtual MasterList Position { get; set; }
        public virtual MasterList VacationPolicy { get; set; }
    }

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

            entity.Property(e => e.FirstName).HasMaxLength(250);
            entity.Property(e => e.LastName).HasMaxLength(250);
            entity.Property(e => e.MiddleName).HasMaxLength(250);
            entity.Property(e => e.Sin).HasMaxLength(11);
            entity.Property(e => e.Email).HasMaxLength(250);

            entity.HasIndex(e => e.DepartmentId);
            entity.HasOne(e => e.Department).WithMany().HasForeignKey(k => k.DepartmentId);

            entity.HasIndex(e => e.WageTypeId);
            entity.HasOne(e => e.WageType).WithMany().HasForeignKey(k => k.WageTypeId);

            entity.HasIndex(e => e.VacationPolicyId);
            entity.HasOne(e => e.VacationPolicy).WithMany().HasForeignKey(k => k.VacationPolicyId);

            entity.HasIndex(e => e.PositionId);
            entity.HasOne(e => e.Position).WithMany().HasForeignKey(k => k.PositionId);

            entity.HasIndex(e => e.AddressId).IsUnique();
            entity.HasOne(e => e.Address).WithOne(a => a.Employee).HasForeignKey<Address>(k => k.Id).IsRequired();

            entity.HasIndex(e => e.UserId);
            entity.HasOne(e => e.User).WithOne(u => u.Employee).HasForeignKey<User>(k => k.Id).IsRequired();
        }
    }
}