using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Managers.Main;
using LibraryManager.Models.BooksShelfModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    [Authorize]
    public class BooksShelfController : Controller
    {
        private readonly SectorManager _sectorManager;
        private readonly SectionManager _sectionManager;
        private readonly IRepository<BooksShelf> _repository;
        private readonly BooksShelfManager _booksShelfManager;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public BooksShelfController(SectorManager sectorManager,
            SectionManager sectionManager,
            IRepository<BooksShelf> repository,
            BooksShelfManager booksShelfManager,
            IMapper mapper, UserManager<Employee> userManager)
        {
            _sectorManager = sectorManager;
            _sectionManager = sectionManager;
            _repository = repository;
            _booksShelfManager = booksShelfManager;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<List<Section>> GetSectionBySector([FromBody] string Id)
        {
            if (Id.Length != 0)
            {
                var filterbyname = await _sectionManager.FilterTableByAsync(Convert.ToInt32(Id), "SectorId");
                return filterbyname;
            }
            return new List<Section>();
        }


        public ActionResult Index()
        {
            BooksShelfVM _booksShelfVm = new BooksShelfVM();
            _booksShelfVm.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            _booksShelfVm.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            _booksShelfVm.SectorsSelectList.AddRange(
                _sectorManager.GetSectorsSelectList());
            return View(_booksShelfVm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BooksShelfVM request)
        {
            var data = await _booksShelfManager.FilterAsync(request);
            List<BooksShelfVM> booksShelves = new List<BooksShelfVM>();
            foreach (var item in data)
            {
                var mapp = _mapper.Map<BooksShelfVM>(item);

                if (item.ModifierId != null && await _userManager.FindByIdAsync(item.ModifierId) != null)
                {
                    var modifierEmployee = await _userManager.FindByIdAsync(item.ModifierId);
                    mapp.ModifierEmployee = modifierEmployee.FirstName + " " + modifierEmployee.LastName;
                }
                else
                    mapp.ModifierEmployee = "";

                if (await _userManager.FindByIdAsync(item.CreatorId) != null)
                {
                    var creatorEmployee = await _userManager.FindByIdAsync(item.CreatorId);
                    mapp.CreatorEmployee = creatorEmployee.FirstName + " " + creatorEmployee.LastName;
                }
              
                
                var section = await _sectionManager.GetSectionById(item.SectionId);
                mapp.Section = section.Name;

                var sector = await _sectorManager.GetSectorByIdAsync(item.SectorId);
                mapp.Sector = sector.Name;

                booksShelves.Add(mapp);
            }

            request.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            request.SectorsSelectList.AddRange(
                _sectorManager.GetSectorsSelectList());
           
            request.SectionsSelectList.AddRange(
                _sectionManager.GetSectionsSelectList(
                    await GetSectionBySector(request.SectorId.ToString())));

            request.BooksShelves.AddRange(booksShelves);
            return View(request);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateBooksShelfVM vm = new CreateBooksShelfVM();
            vm.SectorsSelectList.AddRange(
                _sectorManager.GetSectorsSelectList());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBooksShelfVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fill in all the fields");
                model.SectorsSelectList.AddRange(
                    _sectorManager.GetSectorsSelectList());
                
                model.SectionsSelectList.AddRange(
                    _sectionManager.GetSectionsSelectList(
                        await GetSectionBySector(model.SectorId.ToString())));
                return View(model);
            }

            var filterbyname = await _booksShelfManager.FilterTableByAsync(model.Name, "Name");
            var filterbysection = await _booksShelfManager.FilterTableByAsync(model.SectionId, "SectionId");
            var filterbysector = await _booksShelfManager.FilterTableByAsync(model.SectorId, "SectorId");
            var filterbyLists = _booksShelfManager.FilterLists(filterbyname, filterbysection, filterbysector);

            if (filterbyLists.Any())
                foreach (var item in filterbyLists)
                    if (item.DeleteDate != null)
                        _repository.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This type already exists");
                        model.SectorsSelectList.AddRange(
                            _sectorManager.GetSectorsSelectList());
                        return View(model);
                    }

            var map = _mapper.Map<BooksShelf>(model);
            map.InsertDate = DateTime.Now;
            map.CreatorId = _userManager.GetUserId(User);

            _repository.Insert(map);
            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            if (Id != 0)
            {
                var entity = await _repository.GetByIdAsync(Id);
                if (entity != null)
                {
                    var filterbySectorId = await _sectionManager.FilterTableByAsync(entity.SectorId, "SectorId");
                    var map = _mapper.Map<EditBooksShelfVM>(entity);
                    map.SectorsSelectList.AddRange(
                        _sectorManager.GetSectorsSelectList());
                    map.SectionsSelectList.AddRange(_sectionManager.GetSectionsSelectList(filterbySectorId));
                    return View(map);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditBooksShelfVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fill in all the fields");
                
                model.SectorsSelectList.AddRange(
                        _sectorManager.GetSectorsSelectList());
                
                model.SectionsSelectList.AddRange(
                    _sectionManager.GetSectionsSelectList(
                        await GetSectionBySector(model.SectorId.ToString())));
                return View(model);
            }

            var filterbyname = await _booksShelfManager.FilterTableByAsync(model.Name, "Name");
            var filterbysection = await _booksShelfManager.FilterTableByAsync(model.SectionId, "SectionId");
            var filterbysector = await _booksShelfManager.FilterTableByAsync(model.SectorId, "SectorId");
            var filterbyLists = _booksShelfManager.FilterLists(filterbyname, filterbysection, filterbysector);

            var filterbysectorId = await _sectionManager.FilterTableByAsync(model.SectorId, "SectorId");
            
            model.SectorsSelectList.AddRange(
                _sectorManager.GetSectorsSelectList());
            
            model.SectionsSelectList.AddRange(_sectionManager.GetSectionsSelectList(filterbysectorId));

            if (filterbyLists.Any())
                foreach (var item in filterbyLists)
                    if (item.DeleteDate != null)
                        _repository.Delete(item);
                    else
                    {
                        ModelState.AddModelError("", "This type already exists");
                        return View(model);
                    }

            var entity = await _repository.GetByIdAsync(model.Id);
            var map = _mapper.Map(model, entity);
            
            if (await _userManager.FindByIdAsync(map.CreatorId) == null)
            {
                map.CreatorId = _userManager.GetUserId(User);
            }
            
            map.ModifyDate = DateTime.Now;
            map.ModifierId = _userManager.GetUserId(User);
            _repository.Update(map);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            await _repository.Update_DeleteDate_ByIdAsync(Id);
            return RedirectToAction("Index");
        }
    }
}