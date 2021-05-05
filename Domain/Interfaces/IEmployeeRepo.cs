using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeRepo
    {
        Task<IdentityResult> RegisterAsync(Employee entity, string password);
        Task LogOutAsync();
        Task<SignInResult> LogInAsync(string username, string password);
    }
}
