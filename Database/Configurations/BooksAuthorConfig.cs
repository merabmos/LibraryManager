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
    public class BooksAuthorConfig : IEntityTypeConfiguration<BooksAuthor>
    {
        public void Configure(EntityTypeBuilder<BooksAuthor> builder)
        {
            builder.ToTable("Books_Authors");

            builder.Property(e => e.DeleteDate).HasColumnType("datetime");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("datetime");

            builder.HasOne(d => d.Author)
                .WithMany(p => p.BooksAuthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Authors_B__Autho__37A5467C");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.BooksAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Authors_B__BookI__38996AB5");
        }
    }
}
