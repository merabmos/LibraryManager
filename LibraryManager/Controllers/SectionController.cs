﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Models.SearchModels;
using LibraryManager.Models.SectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [Authorize]
    public class SectionController : Controller
    {
        private readonly IRepository<Section> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly SectorManager _sectorManager;
        private readonly SectionManager _sectionManager;

        public SectionController(IRepository<Section> repository, IMapper mapper,
            UserManager<Employee> userManager, SectorManager sectorManager, SectionManager sectionManager)
        {
            _mapper = mapper;
            _sectorManager = sectorManager;
            _sectionManager = sectionManager;
            _userManager = userManager;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SectionVM sectionVm = new SectionVM();
            sectionVm.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            sectionVm.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            sectionVm.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());

            return View(sectionVm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SectionVM request)
        {
            var data = await _sectionManager.FilterAsync(request);
            List<SectionVM> section = new List<SectionVM>();
            foreach (var item in data)
            {
                var mapp = _mapper.Map<SectionVM>(item);

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

                if (item.SectorId != null)
                {
                    var sector = await _sectorManager.GetSectorById(item.SectorId);
                    if (sector != null)
                        mapp.Sector = sector.Name;
                }

                else
                    mapp.ModifierEmployee = "";

                section.Add(mapp);
            }

            request.Sections.AddRange(section);
            request.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());
            return View(request);
        }

        // GET: SectorController/Create
        public ActionResult Create()
        {
            CreateSectionVM csVM = new CreateSectionVM();
            csVM.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());
            
            return View(csVM);
        }

        // POST: SectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSectionVM model)
        {
            var filterbyname = await _sectionManager.FilterTableByAsync(model.Name, "Name");
            var filterbysector = await _sectionManager.FilterTableByAsync(model.SectorId, "SectorId");
            var filterbyLists = _sectionManager.FilterLists(filterbyname, filterbysector);
    
            if (filterbyLists.Count() > 0)
            {
                foreach(var item in filterbyLists){

               if(item.DeleteDate != null)
               {

               }     
            } 

            }
            else
            {
                var map = _mapper.Map<Section>(model);
                map.InsertDate = DateTime.Now;
            }
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int Id)
        {
            if (Id != 0)
            {
                var entity = _repository.GetById(Id);
                if (entity != null)
                {
                    var map = _mapper.Map<EditSectionVM>(entity);
                    map.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());
                    return View(map);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSectionVM model)
        {
            var filterbyname = await _sectionManager.FilterTableByAsync(model.Name, "Name");
            var filterbysector = await _sectionManager.FilterTableByAsync(model.SectorId, "SectorId");
            var filterbyLists = _sectionManager.FilterLists(filterbyname, filterbysector);
           
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            await _sectionManager.DeleteAsync(Id);
            return RedirectToAction("Index");
        }
    }
}