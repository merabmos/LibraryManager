using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Section
    {
        public Section()
        {
            BooksShelves = new HashSet<BooksShelf>();
        }

        public int Id { get; set; }
        public int? SectorId { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string CreatorId { get; set; }
        public string ModifierId { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual ICollection<BooksShelf> BooksShelves { get; set; }
    }
}
