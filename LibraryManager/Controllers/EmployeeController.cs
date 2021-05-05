using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Models.EmployeeModels;
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


        public EmployeeController(IMapper mapper, IEmployeeManager employeeRepo,
            SignInManager<Employee> signInManager, UserManager<Employee> userManager)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _signInManager = signInManager;
            _userManager = userManager;
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
        public async Task<IActionResult> UpdateUserName()
        {
            var employee = await _userManager.GetUserAsync(User);
            UpdateUserNameVM model = new UpdateUserNameVM()
            {
                UserName = employee.UserName
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserName(UpdateUserNameVM model)
        {
            var employee = await _userManager.GetUserAsync(User);
            employee.UserName = model.UserName;
            var result =  await _employeeRepo.UpdateUserNameAsync(employee);
            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            var entity= await _userManager.GetUserAsync(User);
            var result = await _employeeRepo.ChangePasswordAsync(entity,model.CurrentPassword,model.NewPassword);
            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateName()
        {
            var employee = await _userManager.GetUserAsync(User);
            UpdateName model = new UpdateName()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateName(UpdateName model)
        {
            var employee = await _userManager.GetUserAsync(User);
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Age = model.Age;
            var result = await _employeeRepo.UpdateNameAsync(employee);
            if (result != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    }
}
