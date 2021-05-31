using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class BooksAuthor
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string CreatorId { get; set; }
        public string ModifierId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
