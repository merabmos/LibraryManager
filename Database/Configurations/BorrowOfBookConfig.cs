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
    public class BorrowOfBookConfig : IEntityTypeConfiguration<BorrowOfBook>
    {
        public void Configure(EntityTypeBuilder<BorrowOfBook> builder)
        {
            builder.HasNoKey();

            builder.ToView("BorrowOfBooks");

            builder.Property(e => e.BookName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.BroughtDate).HasColumnType("date");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13)
                .IsUnicode(false);

            builder.Property(e => e.TakenDate).HasColumnType("date");
        }
    }
}
