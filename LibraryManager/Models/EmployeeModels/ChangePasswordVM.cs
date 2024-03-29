﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.EmployeeModels
{
    public class ChangePasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string Current { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string New { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "This Confirm Password Doesn't match The new Password ")]
        public string Confirm { get; set; }

        public bool Valid { get; set; }
        public List<string> ValidationsMessage { get; set; }
        public string SuccessMessage { get; set; }

    }
}
