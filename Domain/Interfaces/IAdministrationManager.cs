using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces
{
    public interface IAdministrationManager
    {
        Task<IdentityResult> CreateRoleAsync(IdentityRole role);
        List<IdentityRole> GetRoles();
        Task<IdentityResult> EditRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<IdentityResult> UpdateInfoAsync(Employee entity);
    }
}