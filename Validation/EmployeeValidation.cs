using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Entities;
using Validation.DTOModels;
using Validation.ResultModels;

namespace Validation
{
   public class EmployeeValidation
    {
        static public ValidationResult RegisterEmployeeValidation(EmployeeRegisterDTO model)
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
    }
}
