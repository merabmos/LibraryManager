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

        public ChangePasswordVM PasswordVM { get; set; } = new ChangePasswordVM();
    }
}
