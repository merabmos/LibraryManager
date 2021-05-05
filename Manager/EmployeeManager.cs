using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Validation;
using Validation.Models;

namespace Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IMapper _mapper;

        public EmployeeManager(UserManager<Employee> userManager,
            SignInManager<Employee> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> LogInAsync(string username, string password)
        {
            var employee = await _userManager.FindByNameAsync(username);
            var result = await _signInManager.PasswordSignInAsync(employee, password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return null;
            }
            return result;
        }

        public async Task<IdentityResult> RegisterAsync(Employee entity, string password)
        {
            var mapp = _mapper.Map<EmployeeVal>(entity);
            var reaction = EmployeeValidation.RegisterEmployeeValidation(mapp);
            IdentityResult identity;
            if (!reaction.Valid)
            {
                return identity = IdentityResult.Failed(
                     new IdentityError[]
                     {
                         new IdentityError{

                             Code = "",
                             Description = reaction.ErrorMessage

                         }
                     }
                );
            }
            else
            {
                entity.ModifyDate = DateTime.Now;
                var result = await _userManager.CreateAsync(entity, password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(entity, true);
                    return null;
                }
                return result;
            }
        }

        public async Task<IdentityResult> UpdateUserNameAsync(Employee entity)
        {
            var mapp = _mapper.Map<EmployeeVal>(entity);
            var reaction = EmployeeValidation.UpdateUserNameValidation(mapp);
            IdentityResult identity;
            if (!reaction.Valid)
            {
                return identity = IdentityResult.Failed(
                     new IdentityError[]
                     {
                         new IdentityError{
                             Code = "",
                             Description = reaction.ErrorMessage
                         }
                     }
                );
            }
            else
            {
                entity.ModifyDate = DateTime.Now;
                var result = await _userManager.UpdateAsync(entity);
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(entity);
                    return null;
                }
                return result;
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(Employee entity, string currentPassword, string newPassword)
        {
            entity.ModifyDate = DateTime.Now;
            var result = await _userManager.ChangePasswordAsync(entity, currentPassword, newPassword);
            if (result.Succeeded)
            { 
            await _signInManager.RefreshSignInAsync(entity);
            return null;
            }
            else
                return result;
        }

        public async Task<IdentityResult> UpdateNameAsync(Employee entity)
        {
            var mapp = _mapper.Map<EmployeeVal>(entity);
            var reaction = EmployeeValidation.UpdateNameValidation(mapp);
            IdentityResult identity;
            if (!reaction.Valid)
            {
                return identity = IdentityResult.Failed(
                     new IdentityError[]
                     {
                         new IdentityError{
                             Code = "",
                             Description = reaction.ErrorMessage
                         }
                     }
                );
            }
            else
            {
                await _signInManager.RefreshSignInAsync(entity);
                await _userManager.UpdateAsync(entity);
                return null;
            }
        }

    }
}
