using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Payroll2.Api.DataAccess.Core.Common;

namespace Payroll2.Api.Environment.DataAccess.EnvironmentEntities
{
    public class Systems : BaseEntity
    {
        public string Name { get; set; }
        public string Connection { get; set; }
        public bool IsSystem { get; set; }
        public bool IsEnable { get; set; }
    }

    public class SystemsConfig : IEntityTypeConfiguration<Systems>
    {
        public void Configure(EntityTypeBuilder<Systems> entity)
        {
            entity.ToTable("Systems");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.Connection).HasMaxLength(250);
            entity.HasIndex(e => e.Connection).IsUnique();
        }
    }
}