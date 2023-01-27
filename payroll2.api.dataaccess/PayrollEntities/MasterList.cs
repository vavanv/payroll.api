using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll2.Api.DataAccess.Core.Common;

namespace Payroll2.Api.DataAccess.PayrollEntities
{
    public class MasterList : BaseEntity
    {
        public int MasterListTypeId { get; set; }
        public string Description { get; set; }
    }

    public class MasterListConfig : IEntityTypeConfiguration<MasterList>
    {
        public void Configure(EntityTypeBuilder<MasterList> entity)
        {
            entity.ToTable("MasterLists");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            entity.HasIndex(e => e.MasterListTypeId);

            entity.Property(e => e.Description).HasMaxLength(250);
        }
    }
}