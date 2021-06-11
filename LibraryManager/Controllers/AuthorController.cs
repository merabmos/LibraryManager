using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using LibraryManager.Managers;
using LibraryManager.Models.AuthorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class AuthorController : Controller
    {
      private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly AuthorManager _authorManager;

        public AuthorController(IMapper mappers,
            UserManager<Employee> userManager, AuthorManager authorManager)
        {
            _mapper = mappers;
            _userManager = userManager;
            _authorManager = authorManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            AuthorVM model = new AuthorVM();
            model.CreatorEmployeesSelectList.AddRange(_authorManager.GetEmployeesSelectList());
            model.ModifierEmployeesSelectList.AddRange(_authorManager.GetEmployeesSelectList());
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(AuthorVM request)
        {
            var data = await _authorManager.FilterAsync(request);
            List<AuthorVM> authors = new List<AuthorVM>();

            foreach (var item in data)
            {
                var mapp = _mapper.Map<AuthorVM>(item);

                if (await _userManager.FindByIdAsync(item.CreatorId) != null)
                {
                    var creatorEmployee = await _userManager.FindByIdAsync(item.CreatorId);
                    mapp.CreatorEmployee = creatorEmployee.FirstName + " " + creatorEmployee.LastName;
                }

                if (item.ModifierId != null && await _userManager.FindByIdAsync(item.ModifierId) != null)
                {
                    var modifierEmployee = await _userManager.FindByIdAsync(item.ModifierId);
                    mapp.ModifierEmployee = modifierEmployee.FirstName + " " + modifierEmployee.LastName;
                }
                else
                    mapp.ModifierEmployee = "";

                authors.Add(mapp);
            }

            request = new AuthorVM();
            request.Authors.AddRange(authors);
            request.CreatorEmployeesSelectList.AddRange(_authorManager.GetEmployeesSelectList());
            request.ModifierEmployeesSelectList.AddRange(_authorManager.GetEmployeesSelectList());
            return View(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateAuthorVM model)
        {
            var map = _mapper.Map<Author>(model);
            
            map.InsertDate = DateTime.Now;
            
            map.CreatorId = _userManager.GetUserId(User);
            
            _authorManager.Insert(map);
            
            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            if (Id != 0)
            {
                var entity = await _authorManager.GetByIdAsync(Id);
                if (entity == null)
                {
                    return RedirectToAction("Index");
                }
                var map = _mapper.Map<EditAuthorVM>(entity);
                return View(map);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAuthorVM model)
        {
            var entity = await _authorManager.GetByIdAsync(model.Id);
            
            var map = _mapper.Map(model, entity);

            if (await _userManager.FindByIdAsync(map.CreatorId) == null)
            {
                map.CreatorId = _userManager.GetUserId(User);
            }
            
            map.ModifyDate = DateTime.Now;

            map.ModifierId = _userManager.GetUserId(User);

            _authorManager.Update(map);
            
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            await _authorManager.Update_DeleteDate_ByIdAsync(Id);
            return RedirectToAction("Index");
        }
    }
}