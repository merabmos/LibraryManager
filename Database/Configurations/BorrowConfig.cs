using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class BorrowConfig : IEntityTypeConfiguration<Borrow>
    {
        public void Configure(EntityTypeBuilder<Borrow> builder)
        {
            builder.ToTable("Borrow");

            builder.Property(e => e.BookCount).HasDefaultValueSql("((1))");

            builder.Property(e => e.DeleteDate).HasColumnType("datetime");

            builder.Property(e => e.InsertDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifyDate).HasColumnType("datetime");

            builder.Property(e => e.ReturnDate).HasColumnType("date");

            builder.Property(e => e.TakenDate).HasColumnType("date");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.Borrows)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Borrow__BookId__2C3393D0");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Borrows)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Borrow__Customer__2B3F6F97");
        }
    }
}