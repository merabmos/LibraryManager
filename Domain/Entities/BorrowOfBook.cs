using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class BorrowOfBook
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string BookName { get; set; }
        public int BookCount { get; set; }
        public string CreatorId { get; set; }
        public string ModifierId { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime BroughtDate { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
