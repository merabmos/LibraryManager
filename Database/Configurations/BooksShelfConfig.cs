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
    public class BooksShelfConfig : IEntityTypeConfiguration<BooksShelf>
    {
        public void Configure(EntityTypeBuilder<BooksShelf> builder)
        {
            builder.Property(e => e.DeleteDate).HasColumnType("datetime");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Section)
                .WithMany(p => p.BooksShelves)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK__BooksShel__Secti__5BAD9CC8").OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Sector)
            .WithMany(p => p.BooksShelves)
            .HasForeignKey(d => d.SectorId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
