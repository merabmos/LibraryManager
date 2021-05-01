using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Borrow
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int BookCount { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
