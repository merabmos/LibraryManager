﻿using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Validations.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Validation.Models;

namespace LibraryManager.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IMapper _mapper;
        private readonly IEmployeeValidation _employeeValidation;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountManager(UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            IMapper mapper, IEmployeeValidation employeeValidation, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _employeeValidation = employeeValidation;
            _roleManager = roleManager;
        }

        public async Task LogOutAsync() =>  await _signInManager.SignOutAsync();


        public async Task<SignInResult> LogInAsync(string username, string password)
        {
            var employee = await _userManager.FindByNameAsync(username);
            if (employee != null)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(employee, password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                    return result;
            }
            return null;
        }

        public async Task<IdentityResult> RegisterAsync(Employee entity, string password)
        {
            var mapp = _mapper.Map<EmployeeVal>(entity);
            var reaction = _employeeValidation.RegisterEmployeeValidation(mapp);
            if (!reaction.Valid)
            {
                return IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "",
                            Description = reaction.ErrorMessage
                        }
                );
            }

            entity.Password = password;
            var result = await _userManager.CreateAsync(entity, password);
            if (result.Succeeded)
            {
                if (_userManager.Users.Count() == 1)
                    if (_roleManager.Roles.Any())
                        await _userManager.AddToRoleAsync(entity, "Super Administrator");
                    else if (_roleManager.Roles.Any())
                        await _userManager.AddToRoleAsync(entity, "Administrator");
                await _signInManager.SignInAsync(entity, true);
                return null;
            }

            return result;
        }

        public async Task<IdentityResult> UpdateDetailsAsync(Employee entity)
        {
            var mapp = _mapper.Map<EmployeeVal>(entity);
            var reactionUserName = _employeeValidation.UpdateUserNameValidation(mapp);
            var reactionDetails = _employeeValidation.UpdateDetailsValidation(mapp);

            if (!reactionUserName.Valid)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "",
                        Description = reactionUserName.ErrorMessage
                    }
                );
            }

            if (!reactionDetails.Valid)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "",
                        Description = reactionDetails.ErrorMessage
                    }
                );
            }

            entity.ModifyDate = DateTime.Now;
            var result = await _userManager.UpdateAsync(entity);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(entity);
                return null;
            }

            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(Employee entity, string currentPassword,
            string newPassword)
        {
            entity.ModifyDate = DateTime.Now;
            entity.Password = newPassword;
            var result = await _userManager.ChangePasswordAsync(entity, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(entity);
                return null;
            }

            return result;
        }
    }
}