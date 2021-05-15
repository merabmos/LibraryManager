﻿using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Models.Sector;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<List<SectorVM>> SearchRecord(string search)
        {
            var data = await _sectorManager.FindBySearchAsync(search);
            List<SectorVM> employees = new List<SectorVM>();
            foreach (var item in data)
            {
                var mapp = _mapper.Map<SectorVM>(item);
                employees.Add(mapp);
            }
            return employees;
        }
        // GET: SectorController
        public  ActionResult Index()
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
            if (data == null)
            {
                var mapp = _mapper.Map<Sector>(model);
                mapp.CreatorEmployeeId = _userManager.GetUserId(User);
                _repository.Insert(mapp);
                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("", "this name already exists");
                return View(model);
            }
        }

        // GET: SectorController/Edit/5
        public ActionResult Edit(int id)
        {
            var sector = _repository.GetById(id);
            var map = _mapper.Map<EditSectorVM>(sector);
            return View(map);
        }

        // POST: SectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSectorVM sectorVM)
        {
            var sector = _repository.GetById(sectorVM.Id);
            sector.Name = sectorVM.Name;
            sector.ModifierEmployeeId = _userManager.GetUserId(User);
            sector.ModifyDate = DateTime.Now;
            _repository.Update(sector);
            return View(sectorVM);
        }

        // GET: SectorController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            await _sectorManager.DeleteAsync(id);
            return RedirectToAction("Index", "Sector");
        }
    }
}
