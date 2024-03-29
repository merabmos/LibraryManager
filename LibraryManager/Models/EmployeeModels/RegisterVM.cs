﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Models.EmployeeModels
{
    public class RegisterVM
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LastName  { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "This Confirm Password Doesn't match Entered Password ")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword  { get; set; }

        [RegularExpression(@"^5{1}[0-9]{2}[0-9]{6}$",
             ErrorMessage = "Characters are not allowed.")]
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
