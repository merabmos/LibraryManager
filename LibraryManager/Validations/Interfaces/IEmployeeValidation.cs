using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Validation.Models;
using Validation.ResultModels;

namespace LibraryManager.Validations.Interfaces
{
    public interface IEmployeeValidation
    {
        Task<ValidationResult> CheckDetailsAsync(ClaimsPrincipal User, string firstName,string lastName, int Age);
        Task<ValidationResult> CheckUserNameAsync(ClaimsPrincipal User, string UserName);
        ValidationResult UpdateDetailsValidation(EmployeeVal model);
        ValidationResult UpdateUserNameValidation(EmployeeVal model);
        ValidationResult RegisterEmployeeValidation(EmployeeVal model);
    }
}
