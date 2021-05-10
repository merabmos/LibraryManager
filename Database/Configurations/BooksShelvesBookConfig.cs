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
    public class BooksShelvesBookConfig : IEntityTypeConfiguration<BooksShelvesBook>
    {
        public void Configure(EntityTypeBuilder<BooksShelvesBook> builder)
        {
            builder.ToTable("BooksShelves_Books");

            builder.Property(e => e.BooksCount).HasDefaultValueSql("((1))");

            builder.Property(e => e.DeleteDate).HasColumnType("date");

            builder.Property(e => e.InsertDate)
                .HasColumnType("date")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("date");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.BooksShelvesBooks)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BooksShel__BookI__09746778");

            builder.HasOne(d => d.BookShelf)
                .WithMany(p => p.BooksShelvesBooks)
                .HasForeignKey(d => d.BookShelfId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BooksShel__BookS__0880433F");
        }
    }
}
