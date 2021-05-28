using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.EmployeeModels
{
    public class EmployeeVM
    {
        public string Id { get; set; }
       
        [Required]
        [RegularExpression(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$")]
        public string UserName { get; set; }
       
        [Required]
        [RegularExpression(@"^[a-zA-Z]+((['][a-zA-Z])?[a-zA-Z]*)*$")]
        public string FirstName { get; set; }
       
        [Required]
        [RegularExpression(@"^[a-zA-Z]+((['][a-zA-Z])?[a-zA-Z]*)*$")]
        public string LastName { get; set; }
        
        [RegularExpression(@"^5{1}[0-9]{2}[0-9]{6}$",
            ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public int Age { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
