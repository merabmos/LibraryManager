using System;
using Database.Configurations;
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

            modelBuilder.ApplyConfiguration(new AuthorConfig());
            modelBuilder.ApplyConfiguration(new BookConfig());
            modelBuilder.ApplyConfiguration(new BooksAuthorConfig());
            modelBuilder.ApplyConfiguration(new BooksGenreConfig());
            modelBuilder.ApplyConfiguration(new BooksShelfConfig());
            modelBuilder.ApplyConfiguration(new BooksShelvesBookConfig());
            modelBuilder.ApplyConfiguration(new BorrowConfig());
            modelBuilder.ApplyConfiguration(new BorrowOfBookConfig());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new GenreConfig());
            modelBuilder.ApplyConfiguration(new SectionConfig());
            modelBuilder.ApplyConfiguration(new SectorConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
