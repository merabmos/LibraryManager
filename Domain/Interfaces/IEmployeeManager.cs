using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeManager
    {
        Task<IdentityResult> RegisterAsync(Employee entity, string password);
        Task<SignInResult> LogInAsync(string username, string password);
        Task LogOutAsync();
        Task<IdentityResult> UpdateDetailsAsync(Employee entity);
        Task<IdentityResult> ChangePasswordAsync(Employee entity , string currentPassword,string newPassword);
    }
}
