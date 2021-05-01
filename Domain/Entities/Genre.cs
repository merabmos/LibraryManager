using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            BooksGenres = new HashSet<BooksGenre>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual ICollection<BooksGenre> BooksGenres { get; set; }
    }
}
