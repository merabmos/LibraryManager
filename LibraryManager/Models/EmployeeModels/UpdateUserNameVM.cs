using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.EmployeeModels
{
    public class UpdateUserNameVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
    }
}
