using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll2.Api.DataAccess.Core.Common;

namespace Payroll2.Api.DataAccess.PayrollEntities
{
    public class Company : BaseEntity
    {
        public string LegalName { get; set; }
        public int BusinessTypeId { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public string PostalCode { get; set; }

        public virtual MasterList Province { get; set; }
        public virtual MasterList BusinessType { get; set; }
    }

    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.ToTable("Company");

            entity.Property(e => e.Id).UseIdentityColumn().ValueGeneratedOnAdd();
            entity.HasKey(e => e.Id);

            entity.Property(e => e.LegalName).HasMaxLength(250);

            entity.HasIndex(e => e.BusinessTypeId);
            entity.HasOne(e => e.BusinessType).WithMany().HasForeignKey(k => k.BusinessTypeId);

            entity.HasIndex(e => e.ProvinceId);
            entity.HasOne(e => e.Province).WithMany().HasForeignKey(k => k.ProvinceId);
        }
    }
}