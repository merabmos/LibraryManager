using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class BooksShelf
    {
        public BooksShelf()
        {
            BooksShelvesBooks = new HashSet<BooksShelvesBook>();
        }

        public int Id { get; set; }
        public int? SectionId { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<BooksShelvesBook> BooksShelvesBooks { get; set; }
    }
}
