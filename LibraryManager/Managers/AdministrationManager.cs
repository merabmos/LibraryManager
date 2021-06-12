using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Models.EmployeeModels;

namespace LibraryManager.Managers
{
    public class AdministrationManager : IAdministrationManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;

        public AdministrationManager(RoleManager<IdentityRole> roleManager, UserManager<Employee> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
        {
            // Saves the role in the underlying AspNetRoles table
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public List<IdentityRole> GetRoles()
        {
            return _roleManager.Roles?.ToList();
        }

        public async Task<IdentityResult> EditRoleAsync(IdentityRole role)
        {
            IdentityResult result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole role)
        {
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public async Task<IdentityResult> UpdateInfoAsync(Employee employee)
        {
            employee.ModifyDate = DateTime.Now;
            var result = await _userManager.UpdateAsync(employee);
            if (result.Succeeded)
            {
                return null;
            }

            return result;
        }

        public async Task<bool> IsInRoleAsync(Employee employee, string role)
        {
            if (await _userManager.IsInRoleAsync(employee, role))
            {
                return true;
            }

            return false;
        }
    }
}