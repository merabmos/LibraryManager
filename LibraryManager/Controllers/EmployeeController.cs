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
        private readonly IEmployeeRepo _employeeRepo;
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;

        public EmployeeController(IMapper mapper, IEmployeeRepo employeeRepo,
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
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }

        }
    }
}
