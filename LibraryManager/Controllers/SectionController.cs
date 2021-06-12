using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Managers;
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
            sectionVm.SectorsSelectList.AddRange(_sectorManager.GetEntitiesSelectList());

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

                var sector = await _sectorManager.GetByIdAsync(item.SectorId);
                if (sector != null)
                    mapp.Sector = sector.Name;

                section.Add(mapp);
            }

            request.Sections.AddRange(section);
            request.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.SectorsSelectList.AddRange(_sectorManager.GetEntitiesSelectList());
            return View(request);
        }

        // GET: SectorController/Create
        public ActionResult Create()
        {
            CreateSectionVM csVM = new CreateSectionVM();
            csVM.SectorsSelectList.AddRange(_sectorManager.GetEntitiesSelectList());

            return View(csVM);
        }

        // POST: SectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSectionVM model)
        {
            var filterbyname = await _sectionManager.FilterOfEntititesByValue(model.Name, "Name");
            var filterbysector = await _sectionManager.FilterOfEntititesByValue(model.SectorId, "SectorId");
            var filterbyLists = _sectionManager.Intersect(filterbyname, filterbysector);

                foreach (var item in filterbyLists)
                    if (item.DeleteDate != null)
                        _repository.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This type already exists");
                        model.SectorsSelectList.AddRange(_sectorManager.GetEntitiesSelectList());
                        return View(model);
                    }

            var map = _mapper.Map<Section>(model);
            map.InsertDate = DateTime.Now;
            map.CreatorId = _userManager.GetUserId(User);
            _repository.Insert(map);

            return RedirectToAction("Create");
        }

        public ActionResult Edit(int Id)
        {
            if (Id != 0)
            {
                var entity = _repository.GetByIdAsync(Id);
                if (entity != null)
                {
                    var map = _mapper.Map<EditSectionVM>(entity);
                    map.SectorsSelectList.AddRange(_sectorManager.GetEntitiesSelectList());
                    return View(map);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSectionVM model)
        {
            var filterbyname = await _sectionManager.FilterOfEntititesByValue(model.Name, "Name");
            var filterbysector = await _sectionManager.FilterOfEntititesByValue(model.SectorId, "SectorId");
            var filterbyLists = _sectionManager.Intersect(filterbyname, filterbysector);
                
            foreach (var item in filterbyLists)
                    if (item.DeleteDate != null)
                        _repository.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This type already exists");
                        model.SectorsSelectList.AddRange(_sectorManager.GetEntitiesSelectList());
                        return View(model);
                    }

            var entity = await _repository.GetByIdAsync(model.Id);
            var map = _mapper.Map(model, entity);
            if (await _userManager.FindByIdAsync(map.CreatorId) == null)
            {
                map.CreatorId = _userManager.GetUserId(User);
            }

            map.ModifierId = _userManager.GetUserId(User);
            map.ModifyDate = DateTime.Now;
            _repository.Update(map);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int Id)
        {
            await _repository.Update_DeleteDate_ByIdAsync(Id);
            return RedirectToAction("Index");
        }
    }
}