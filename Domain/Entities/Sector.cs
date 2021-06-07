using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Sector
    {
        public Sector()
        {
            Sections = new HashSet<Section>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string CreatorId { get; set; }
        public string ModifierId { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<BooksShelf> BooksShelves { get; set; }

    }
}
