using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Configurations
{
    public class SectionConfig : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasIndex(e => e.Name, "UQ__Sections__737584F660D4979F")
                    .IsUnique();

            builder.Property(e => e.DeleteDate).HasColumnType("datetime");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Sector)
                .WithMany(p => p.Sections)
                .HasForeignKey(d => d.SectorId)
                .HasConstraintName("FK__Sections__Sector__5CA1C101");
        }
    }
}
