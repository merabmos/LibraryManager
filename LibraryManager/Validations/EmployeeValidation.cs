﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using LibraryManager.Validations.Interfaces;
using Microsoft.AspNetCore.Identity;
using Validation.Models;
using Validation.ResultModels;

namespace Validation
{
    public class EmployeeValidation : IEmployeeValidation
    {
        private readonly UserManager<Employee> _userManager;
        public EmployeeValidation(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ValidationResult> CheckUserNameAsync(ClaimsPrincipal User, string UserName)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var user = await _userManager.FindByNameAsync(UserName);
            ValidationResult result = new ValidationResult();

            if (user != null)
            {
                if (UserName == currentUser.UserName)
                {
                    result.Valid = false;
                    result.ErrorMessage = "You have this username already";
                    return result;
                }
                else
                {
                    result.Valid = false;
                    result.ErrorMessage = "Someone has this username";
                    return result;
                }
            }
            else
            {
                result.Valid = true;
                return result;
            }

        }

        public ValidationResult UpdateDetailsValidation(EmployeeVal model)
        {
            var regexName = new Regex("^[a-zA-Z]+((['][a-zA-Z])?[a-zA-Z]*)*$");
            ValidationResult result = new ValidationResult();
            if (!regexName.IsMatch(model.FirstName))
            {
                result.ErrorMessage = "FirstName is not valid";
                result.Valid = false;
                return result;
            }
            else if (!regexName.IsMatch(model.LastName))
            {
                result.ErrorMessage = "LastName is not valid";
                result.Valid = false;
                return result;
            }
            else if (model.Age <= 16 || model.Age >= 80)
            {
                result.ErrorMessage = "Age must be more than 16 and less than 80";
                result.Valid = false;
                return result;
            }
            else
            {
                result.Valid = true;
                return result;
            }
        }

        public ValidationResult UpdateUserNameValidation(EmployeeVal model)
        {
            var regexUserName = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$");
            ValidationResult result = new ValidationResult();
            if (!regexUserName.IsMatch(model.UserName))
            {
                result.ErrorMessage = "UserName is not valid";
                result.Valid = false;
                return result;
            }
            else
            {
                result.Valid = true;
                return result;
            }
        }

        public ValidationResult RegisterEmployeeValidation(EmployeeVal model)
        {
            var regexName = new Regex("^[a-zA-Z]+((['][a-zA-Z])?[a-zA-Z]*)*$");
            var regexUserName = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$");
            ValidationResult result = new ValidationResult();
            if (!regexName.IsMatch(model.FirstName))
            {
                result.ErrorMessage = "FirstName is not valid";
                result.Valid = false;
                return result;
            }
            else if (!regexName.IsMatch(model.LastName))
            {
                result.ErrorMessage = "LastName is not valid";
                result.Valid = false;
                return result;
            }
            else if (!regexUserName.IsMatch(model.UserName))
            {
                result.ErrorMessage = "UserName is not valid";
                result.Valid = false;
                return result;
            }
            else if (model.Age <= 16 || model.Age >= 80)
            {
                result.ErrorMessage = "Age must be more than 16 and less than 80";
                result.Valid = false;
                return result;
            }
            else
            {
                result.Valid = true;
                return result;
            }
        }

        public async Task<ValidationResult> CheckDetailsAsync(ClaimsPrincipal User, string firstName, string lastName, int Age)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ValidationResult result = new ValidationResult();
            if (firstName == currentUser.FirstName)
            {
                result.ErrorMessage = "You have this firstname already";
                result.Valid = false;
                return result;
            }
            else if (lastName == currentUser.LastName)
            {
                result.ErrorMessage = "You have this lastname already";
                result.Valid = false;
                return result;
            }
            else if (Age == currentUser.Age)
            {
                result.ErrorMessage = "You have this age already";
                result.Valid = false;
                return result;
            }
            else
            {
                result.Valid = true;
                return result;
            }
        }
    }
}
