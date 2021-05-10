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
   public class BooksGenreConfig : IEntityTypeConfiguration<BooksGenre>
    {
        public void Configure(EntityTypeBuilder<BooksGenre> builder)
        {
            builder.ToTable("Books_Genres");

            builder.Property(e => e.DeleteDate).HasColumnType("datetime");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("datetime");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.BooksGenres)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Books_Gen__Genre__3B75D760");

            builder.HasOne(d => d.Genre)
                .WithMany(p => p.BooksGenres)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Books_Gen__BookI__3C69FB99");
        }
    }
}
