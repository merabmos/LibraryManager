using Domain.Entities;
using LibraryManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using LibraryManager.Models.EmployeeModels;
using LibraryManager.Models.RoleModels;
using LibraryManager.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace LibraryManager.Controllers
{
    [Authorize(Roles = "Super Administrator")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Employee> _userManager;
        private readonly IAdministrationManager _administrationManager;
        private readonly IMapper _mapper;

        private readonly IAccountManager _accountManager;

        // GET: Role
        public AdministrationController(RoleManager<IdentityRole> roleManager,
            IAdministrationManager administrationManager, UserManager<Employee> userManager, IMapper mapper,
             IAccountManager accountManager)
        {
            _roleManager = roleManager;
            _administrationManager = administrationManager;
            _userManager = userManager;
            _mapper = mapper;
            _accountManager = accountManager;
        }

        public ActionResult Index()
        {
            var dbroles = _administrationManager.GetRoles();
            List<RoleVM> roles = new List<RoleVM>();
            if (dbroles != null)
            {
                foreach (var item in dbroles)
                {
                    RoleVM role = new RoleVM();
                    role.Id = item.Id;
                    role.Name = item.Name;
                    roles.Add(role);
                }
            }

            return View(roles);
        }

        // GET: Role/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleVM model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.Name
                };

                var result = await _administrationManager.CreateRoleAsync(role);
                if (result == null)
                {
                    return View();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        // GET: Role/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                EditRoleVM roleVm = new EditRoleVM();
                roleVm.Id = role.Id;
                roleVm.Name = role.Name;
                return View(roleVm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditRoleVM model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                role.Name = model.Name;
                var result = await _administrationManager.EditRoleAsync(role);
                if (result == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var result = await _administrationManager.DeleteRoleAsync(role);
            if (result == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            ViewBag.roleId = id;
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Role = role.Name;
            var model = new List<EmployeeRoleVM>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new EmployeeRoleVM
                {
                    EmployeeId = user.Id,
                    Name = user.FirstName + " " + user.LastName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(List<EmployeeRoleVM> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction("Index");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].EmployeeId);

                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Details", new {id = id});
                }
            }

            return RedirectToAction("Details", new {id = id});
        }

        public IActionResult Employees(string user)
        {
            var employees = _userManager.Users.Where(o => o.Id != _userManager.GetUserId(User)).ToList();
            var employeesVM = new List<EmployeeVM>();
            if (employees.Count() > 0)
            {
                foreach (var record in employees)
                {
                    var mapped = _mapper.Map<EmployeeVM>(record);
                    employeesVM.Add(mapped);
                }

                return View(employeesVM);
            }
            else
            {
                return View(employeesVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageEmployeeInfo(string employeeId)
        {
            ViewBag.employeeId = employeeId;

            var user = await _userManager.FindByIdAsync(employeeId);

            if (user == null)
            {
                return RedirectToAction("Employees");
            }

            var mapped = _mapper.Map<EmployeeVM>(user);
            return View(mapped);
        }

        [HttpPost]
        public async Task<IActionResult> ManageEmployeeInfo(EmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _userManager.FindByIdAsync(model.Id);
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Age = model.Age;
                employee.PhoneNumber = model.PhoneNumber;
                var result = await _administrationManager.UpdateInfoAsync(employee);
                if (result == null)
                {
                    return RedirectToAction("Employees");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ManageEmployeeRoles(string employeeId)
        {
            ViewBag.employeeId = employeeId;

            var user = await _userManager.FindByIdAsync(employeeId);

            if (user == null)
            {
                return RedirectToAction("Employees");
            }

            var model = new List<EmployeeRolesVM>();
            
            foreach (var role in _roleManager.Roles)
            {
                var userRolesVM = new EmployeeRolesVM()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesVM.IsSelected = true;
                }
                else
                {
                    userRolesVM.IsSelected = false;
                }

                model.Add(userRolesVM);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>
            ManageEmployeeRoles(List<EmployeeRolesVM> model, string employeeId)
        {
            var user = await _userManager.FindByIdAsync(employeeId);

            if (user == null)
            {
                return RedirectToAction("Employees");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Employees");
        }
    }
}