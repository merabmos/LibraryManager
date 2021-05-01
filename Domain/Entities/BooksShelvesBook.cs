using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class BooksShelvesBook
    {
        public int Id { get; set; }
        public int BookShelfId { get; set; }
        public int BookId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public int BooksCount { get; set; }

        public virtual Book Book { get; set; }
        public virtual BooksShelf BookShelf { get; set; }
    }
}
