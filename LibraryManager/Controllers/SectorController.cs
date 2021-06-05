using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Models.SectorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Controllers
{
    [Authorize]
    public class SectorController : Controller
    {
        private readonly IRepository<Sector> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly SectorManager _sectorManager;

        public SectorController(IRepository<Sector> repository, IMapper mappers,
            UserManager<Employee> userManager, SectorManager sectorManager)
        {
            _repository = repository;
            _mapper = mappers;
            _userManager = userManager;
            _sectorManager = sectorManager;
        }

        [HttpPost]
        public async Task<ActionResult> Index(SectorVM request)
        {
            var data = await _sectorManager.FilterAsync(request);
            List<SectorVM> sectors = new List<SectorVM>();

            foreach (var item in data)
            {
                var mapp = _mapper.Map<SectorVM>(item);

                if (item.CreatorId != null)
                {
                    var creatorEmployee = await _userManager.FindByIdAsync(item.CreatorId);
                    mapp.CreatorEmployee = creatorEmployee.FirstName + " " + creatorEmployee.LastName;
                }

                if (item.ModifierId != null)
                {
                    var modifierEmployee = await _userManager.FindByIdAsync(item.ModifierId);
                    mapp.ModifierEmployee = modifierEmployee.FirstName + " " + modifierEmployee.LastName;
                }
                else
                    mapp.ModifierEmployee = "";

                sectors.Add(mapp);
            }

            request = new SectorVM();
            request.Sectors.AddRange(sectors);
            request.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            return View(request);
        }

        // GET: SectorController 
        [HttpGet]
        public ActionResult Index()
        {
            SectorVM sectorVM = new SectorVM();
            sectorVM.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            sectorVM.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());

            return View(sectorVM);
        }

        // GET: SectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSectorVM model)
        {
            var data = await _sectorManager.FindBySearchAsync(model.Name);
            if (data.Any())
                foreach (var item in data)
                    if (item.DeleteDate != null)
                        _repository.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This name already exists");
                        return View(model);
                    }
               
            var map = _mapper.Map<Sector>(model);
            map.InsertDate = DateTime.Now;
            map.CreatorId = _userManager.GetUserId(User);
            _repository.Insert(map);
            return RedirectToAction("Create");
        }

        // GET: SectorController/Edit/5
        public ActionResult Edit(int Id)
        {
            if (Id != 0)
            {
                var sector = _repository.GetByIdAsync(Id);
                if (sector != null)
                {
                    var map = _mapper.Map<EditSectorVM>(sector);
                    return View(map);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSectorVM model)
        {
            var data = await _sectorManager.FindBySearchAsync(model.Name);
            if (data.Any())
                foreach (var item in data)
                    if (item.DeleteDate != null)
                        _repository.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This Name already exists");
                        return View(model);
                    }
            
            var entity = _repository.GetByIdAsync(model.Id);
            var map = _mapper.Map(model, entity);
            map.ModifyDate = DateTime.Now;
            map.ModifierId = _userManager.GetUserId(User);
            _repository.Update(map);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            await _sectorManager.RemoveByIdAsync(Id);
            return RedirectToAction("Index");
        }
    }
}