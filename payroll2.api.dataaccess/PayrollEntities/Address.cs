using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll2.Api.DataAccess.Core.Common;

namespace Payroll2.Api.DataAccess.PayrollEntities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public string PostalCode { get; set; }

        public virtual MasterList Province { get; set; }

        public virtual Employee Employee { get; set; }
    }

    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("Addresses");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

            entity.Property(e => e.Street).IsRequired().HasMaxLength(250);
            entity.Property(e => e.City).IsRequired().HasMaxLength(250);
            entity.Property(e => e.PostalCode).HasMaxLength(7);

            entity.HasIndex(e => e.ProvinceId);
            entity.HasOne(e => e.Province).WithMany().HasForeignKey(k => k.ProvinceId);
        }
    }
}