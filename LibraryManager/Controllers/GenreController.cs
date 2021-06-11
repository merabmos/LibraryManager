using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Models.GenreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class GenreController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IMapper _mapper;
        private readonly GenreManager _genreManager;

        public GenreController(IMapper mappers,
            UserManager<Employee> userManager, GenreManager sectorManager)
        {
            _mapper = mappers;
            _userManager = userManager;
            _genreManager = sectorManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            GenreVM model = new GenreVM();
            model.CreatorEmployeesSelectList.AddRange(_genreManager.GetEmployeesSelectList());
            model.ModifierEmployeesSelectList.AddRange(_genreManager.GetEmployeesSelectList());
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(GenreVM request)
        {
            var data = await _genreManager.FilterAsync(request);
            List<GenreVM> genres = new List<GenreVM>();

            foreach (var item in data)
            {
                var mapp = _mapper.Map<GenreVM>(item);

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

                genres.Add(mapp);
            }

            request = new GenreVM();
            request.Genres.AddRange(genres);
            request.CreatorEmployeesSelectList.AddRange(_genreManager.GetEmployeesSelectList());
            request.ModifierEmployeesSelectList.AddRange(_genreManager.GetEmployeesSelectList());
            return View(request);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGenreVM model)
        {
            var data = await _genreManager.FilterOfTableByAsync(model.Name, "Name");
            if (data.Any())
                foreach (var item in data)
                    if (item.DeleteDate != null)
                        _genreManager.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This name already exists");
                        return View(model);
                    }

            var map = _mapper.Map<Genre>(model);
            map.InsertDate = DateTime.Now;
            map.CreatorId = _userManager.GetUserId(User);
            _genreManager.Insert(map);
            return RedirectToAction("Create");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            if (Id != 0)
            {
                var entity = await _genreManager.GetByIdAsync(Id);
                var map = _mapper.Map<EditGenreVM>(entity);
                return View(map);
            }
            return RedirectToAction("Index");
        }

        // POST: Genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditGenreVM model)
        {
            var data = await _genreManager.FilterOfTableByAsync(model.Name, "Name");
            if (data.Any())
                foreach (var item in data)
                    if (item.DeleteDate != null)
                        _genreManager.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This Name already exists");
                        return View(model);
                    }

            var entity = await _genreManager.GetByIdAsync(model.Id);

            var map = _mapper.Map(model, entity);
            
            if (await _userManager.FindByIdAsync(map.CreatorId) == null)
            {
                map.CreatorId = _userManager.GetUserId(User);
            }

            
            map.ModifyDate = DateTime.Now;

            map.ModifierId = _userManager.GetUserId(User);

            _genreManager.Update(map);

            return RedirectToAction("Index");
        }

   

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            await _genreManager.Update_DeleteDate_ByIdAsync(Id);
            return RedirectToAction("Index");
        }
    }
}