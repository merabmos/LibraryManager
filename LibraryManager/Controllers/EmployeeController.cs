using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Models.EmployeeModels;
using LibraryManager.Validations.Interfaces;
using Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeManager _employeeRepo;
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeValidation _employeeValidation;

        public EmployeeController(IMapper mapper, IEmployeeManager employeeRepo,
            SignInManager<Employee> signInManager, UserManager<Employee> userManager,
            IEmployeeValidation employeeValidation)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _signInManager = signInManager;
            _userManager = userManager;
            _employeeValidation = employeeValidation;
        }


        [HttpGet]
        [Authorize]
        public IActionResult LogOut()
        {
            if (_signInManager.IsSignedIn(User))
                _employeeRepo.LogOutAsync();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult LogIn()
        {
            if (!_signInManager.IsSignedIn(User))
                return View();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInVM model)
        {
            var result = await _employeeRepo.LogInAsync(model.UserName, model.Password);
            if (result != null)
            {
                ModelState.AddModelError("", "Password or UserName is wrong!");
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var mapp = _mapper.Map<Employee>(model);
            var result = await _employeeRepo.RegisterAsync(mapp, model.Password);
            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details()
        {
            var employee = await _userManager.GetUserAsync(User);
            var mapp = _mapper.Map<DetailsVM>(employee);
            return View(mapp);
        }

        [HttpPost]
        public async Task<IActionResult> Details(DetailsVM detailsVM)
        {
            //Check username exist or not
            var reaction = await _employeeValidation.CheckUserNameAsync(User,detailsVM.UserName);
            var currentEmployee = await _userManager.GetUserAsync(User);
            if (reaction.Valid)
            {
                    currentEmployee.UserName = detailsVM.UserName;
                    currentEmployee.FirstName = detailsVM.FirstName;
                    currentEmployee.LastName = detailsVM.LastName;
                    currentEmployee.Age = detailsVM.Age;
                    var result = await _employeeRepo.UpdateDetailsAsync(currentEmployee);
                    if (result != null)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
            }
            else
            {
                ModelState.AddModelError("", reaction.ErrorMessage);
            }

            return View(detailsVM);
        }

        [HttpPost]
        public async Task<ChangePasswordVM> ChangePassword(ChangePasswordVM change)
        {
            if (ModelState.IsValid)
            {
                var entity = await _userManager.GetUserAsync(User);
                var result = await _employeeRepo.ChangePasswordAsync(entity, change.CurrentPassword, change.CurrentPassword);
                if (result != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                     return null;
                }
            }

            return change;
        }
    }
}
