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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Controllers
{
    public class BooksShelfController : Controller
    {
        private readonly SectorManager _sectorManager;
        private readonly SectionManager _sectionManager;
        private readonly IRepository<BooksShelf> _repository;
        private readonly BooksShelfManager _booksShelfManager;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        private readonly BooksShelfVM _booksShelfVm;

        public BooksShelfController(SectorManager sectorManager,
            SectionManager sectionManager,
            IRepository<BooksShelf> repository,
            BooksShelfManager booksShelfManager,
            IMapper mapper, UserManager<Employee> userManager, BooksShelfVM booksShelfVm)
        {
            _sectorManager = sectorManager;
            _sectionManager = sectionManager;
            _repository = repository;
            _booksShelfManager = booksShelfManager;
            _mapper = mapper;
            _userManager = userManager;
            _booksShelfVm = booksShelfVm;
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
            _booksShelfVm.CreatorEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            _booksShelfVm.ModifierEmployeesSelectList.AddRange(_repository.GetEmployeesSelectList());
            _booksShelfVm.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());
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

                if (item.SectionId != null)
                {
                    
                    var section = await _sectionManager.GetSectionById(item.SectionId);
                    mapp.Section = section.Name;
                    var sector =  await _sectorManager.GetSectorByIdAsync(section.SectorId); 
                    if (sector != null)
                        mapp.Sector = sector.Name;
                }
                
                else
                    mapp.ModifierEmployee = "";
                
                booksShelves.Add(mapp);
            }

            BooksShelfVM.BooksShelves.Clear();
            BooksShelfVM.BooksShelves.AddRange(booksShelves);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateBooksShelfVM vm = new CreateBooksShelfVM();
            vm.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBooksShelfVM model)
        {
                var filterbyname = await _booksShelfManager.FilterTableByAsync(model.Name, "Name");
                var filterbysection = await _booksShelfManager.FilterTableByAsync(model.SectionId, "SectionId");
                var filterbyLists = _booksShelfManager.FilterLists(filterbyname, filterbysection);
                
                if (filterbyLists.Any())
                    foreach (var item in filterbyLists)
                        if (item.DeleteDate != null)
                            _repository.Delete(item);
                        else
                        {
                            ModelState.AddModelError("", "This type already exists");
                            model.SectorsSelectList.AddRange(_sectorManager.GetSectorsSelectList());
                            return View(model);
                        }
               
                var map = _mapper.Map<BooksShelf>(model);
                map.InsertDate = DateTime.Now;
                map.CreatorId = _userManager.GetUserId(User);
                _repository.Insert(map);
                return RedirectToAction("Create");
        }
    
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
/*
        // GET: BooksShelf/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }*/

        // POST: BooksShelf/Delete/5
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
        public IActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}