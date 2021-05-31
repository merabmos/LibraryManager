using System;
using System.Collections.Generic;


namespace Domain.Entities
{
    public partial class Book
    {
        public Book()
        {
            BooksAuthors = new HashSet<BooksAuthor>();
            BooksGenres = new HashSet<BooksGenre>();
            BooksShelvesBooks = new HashSet<BooksShelvesBook>();
            Borrows = new HashSet<Borrow>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public int CurrentCount { get; set; }
        public string CreatorId { get; set; }
        public string ModifierId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual ICollection<BooksAuthor> BooksAuthors { get; set; }
        public virtual ICollection<BooksGenre> BooksGenres { get; set; }
        public virtual ICollection<BooksShelvesBook> BooksShelvesBooks { get; set; }
        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
