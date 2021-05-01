using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Borrows = new HashSet<Borrow>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual ICollection<Borrow> Borrows { get; set; }
    }
}
