using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace Database
{
    public partial class LibraryManagerDBContext : IdentityDbContext<Employee>
    {
        public LibraryManagerDBContext(DbContextOptions<LibraryManagerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BooksAuthor> BooksAuthors { get; set; }
        public virtual DbSet<BooksGenre> BooksGenres { get; set; }
        public virtual DbSet<BooksShelf> BooksShelves { get; set; }
        public virtual DbSet<BooksShelvesBook> BooksShelvesBooks { get; set; }
        public virtual DbSet<Borrow> Borrows { get; set; }
        public virtual DbSet<BorrowOfBook> BorrowOfBooks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.CurrentCount).HasColumnName("Current_Count");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCount).HasColumnName("Total_Count");
            });

            modelBuilder.Entity<BooksAuthor>(entity =>
            {
                entity.ToTable("Books_Authors");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BooksAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Authors_B__Autho__37A5467C");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BooksAuthors)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Authors_B__BookI__38996AB5");
            });

            modelBuilder.Entity<BooksGenre>(entity =>
            {
                entity.ToTable("Books_Genres");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BooksGenres)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Books_Gen__Genre__3B75D760");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.BooksGenres)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Books_Gen__BookI__3C69FB99");
            });

            modelBuilder.Entity<BooksShelf>(entity =>
            {
                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.BooksShelves)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK__BooksShel__Secti__5BAD9CC8");
            });

            modelBuilder.Entity<BooksShelvesBook>(entity =>
            {
                entity.ToTable("BooksShelves_Books");

                entity.Property(e => e.BooksCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DeleteDate).HasColumnType("date");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BooksShelvesBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BooksShel__BookI__09746778");

                entity.HasOne(d => d.BookShelf)
                    .WithMany(p => p.BooksShelvesBooks)
                    .HasForeignKey(d => d.BookShelfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BooksShel__BookS__0880433F");
            });

            modelBuilder.Entity<Borrow>(entity =>
            {
                entity.ToTable("Borrow");

                entity.Property(e => e.BookCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.TakenDate).HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Borrow__BookId__2C3393D0");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Borrows)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Borrow__Customer__2B3F6F97");
            });

            modelBuilder.Entity<BorrowOfBook>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BorrowOfBooks");

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BroughtDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.TakenDate).HasColumnType("date");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Sections__737584F660D4979F")
                    .IsUnique();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SectorId)
                    .HasConstraintName("FK__Sections__Sector__5CA1C101");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Sectors__737584F6D5CA3577")
                    .IsUnique();

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.InsertDate)
                     .HasColumnType("datetime")
                     .HasDefaultValueSql("(getdate())");
                entity.Property(e => e.DeleteDate).HasColumnType("datetime");
                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
