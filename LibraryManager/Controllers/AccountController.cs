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
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAccountManager _employeeRepo;
        private readonly SignInManager<Employee> _signInManager;
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeValidation _employeeValidation;
        private readonly IRepository<Employee> _repository;

        public AccountController(IMapper mapper, IAccountManager employeeRepo,
            SignInManager<Employee> signInManager, UserManager<Employee> userManager,
            IEmployeeValidation employeeValidation, IRepository<Employee> repository)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            _signInManager = signInManager;
            _userManager = userManager;
            _employeeValidation = employeeValidation;
            _repository = repository;
        }

        [Authorize]
        public ActionResult Index()
        {
            var employees = _repository.GetAll();
            List<EmployeeVM> employeeVM = new List<EmployeeVM>();
            foreach (var item in employees)
                if (item.DeleteDate == null)
                {
                    var map = _mapper.Map<EmployeeVM>(item);
                    employeeVM.Add(map);
                }
            return View(employeeVM);
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
            if (result == null)
            {
                ModelState.AddModelError("", "Password or UserName is wrong!");
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (!_signInManager.IsSignedIn(User))
                return View();
            return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
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
            var reaction = await _employeeValidation.CheckUserNameAsync(User, detailsVM.UserName);
            var currentEmployee = await _userManager.GetUserAsync(User);
            currentEmployee.FirstName = detailsVM.FirstName;
            currentEmployee.LastName = detailsVM.LastName;
            currentEmployee.Age = detailsVM.Age;
            currentEmployee.PhoneNumber = detailsVM.PhoneNumber;

            if (reaction.Valid)
            {
                currentEmployee.UserName = detailsVM.UserName;

                var result = await _employeeRepo.UpdateDetailsAsync(currentEmployee);

                if (result != null)

                    foreach (var error in result.Errors)

                        ModelState.AddModelError("", error.Description);
            }
            else
            {
                var result = await _employeeRepo.UpdateDetailsAsync(currentEmployee);

                if (result != null)

                    foreach (var error in result.Errors)

                        ModelState.AddModelError("", error.Description);

                ModelState.AddModelError("", reaction.ErrorMessage + " but other informations are updated");
            }

            return View(detailsVM);
        }

        [HttpPost]
        public async Task<ChangePasswordVM> ChangePassword([FromBody] ChangePasswordVM request)
        {
            try
            {
                ChangePasswordVM passwordVM = new ChangePasswordVM();
                passwordVM.ValidationsMessage = new List<string>();
                var reaction = await _employeeValidation.ChangePasswordValidationsAsync(User, request);
                if (reaction.Valid)
                {
                    var entity = await _userManager.GetUserAsync(User);
                    var result = await _employeeRepo.ChangePasswordAsync(entity, request.Current, request.New);
                    if (result != null)
                    {
                        passwordVM.Valid = false;
                        foreach (var error in result.Errors)
                            passwordVM.ValidationsMessage.Add(error.Description);
                        return passwordVM;
                    }
                    else
                    {
                        passwordVM.Valid = true;
                        passwordVM.SuccessMessage = "Password changed successfully";
                        return passwordVM;
                    }
                }
                else
                {
                    passwordVM.Valid = false;
                    passwordVM.ValidationsMessage.Add(reaction.ErrorMessage);
                    return passwordVM;
                }
            }
            catch
            {
                ChangePasswordVM passwordVM = new ChangePasswordVM();
                passwordVM.ValidationsMessage = new List<string>();
                passwordVM.ValidationsMessage.Add("A problem has been fixed");
                return passwordVM;
            }
        }
    }
}
