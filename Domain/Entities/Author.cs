using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Author
    {
        public Author()
        {
            BooksAuthors = new HashSet<BooksAuthor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CreatorEmployeeId { get; set; }
        public string ModifierEmployeeId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual ICollection<BooksAuthor> BooksAuthors { get; set; }
    }
}
