using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Validation;
using Validation.DTOModels;

namespace Manager
{
    public class EmployeeManager : IEmployeeRepo
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

        public async Task<IdentityResult> RegisterAsync(Employee entity, string password)
        {
            var mapp = _mapper.Map<EmployeeRegisterDTO>(entity);
            var error = EmployeeValidation.RegisterEmployeeValidation(mapp);
            IdentityResult identity;
            if (!error.Valid)
            {
                return identity = IdentityResult.Failed(
                     new IdentityError[]
                     {
                         new IdentityError{

                             Code = "",
                             Description = error.ErrorMessage

                         }
                     }
                );
            }
            else
            {
                var result = await _userManager.CreateAsync(entity, password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(entity,true);
                }
                return result;
            }
        }
    }
}
