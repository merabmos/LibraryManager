using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.EmployeeModels
{
    public class DetailsVM
    {
      
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [RegularExpression(@"^5{1}[0-9]{2}[0-9]{6}$",
            ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        public ChangePasswordVM PasswordVM { get; set; } = new ChangePasswordVM();

    }
}
