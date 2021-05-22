using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers;
using LibraryManager.Models.SearchModels;
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
using Newtonsoft.Json;
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
        public async Task<List<SectorVM>> SearchRecord([FromBody] FilterVM request)
        {
            var data =  await _sectorManager.FilterAsync(request);
            List<SectorVM> sectors = new List<SectorVM>();
            foreach (var item in data)
            {
                var mapp = _mapper.Map<SectorVM>(item);

                if (item.CreatorEmployeeId != null)
                {
                    var creatorEmployee = await _userManager.FindByIdAsync(item.CreatorEmployeeId);
                    mapp.CreatorEmployee = creatorEmployee.FirstName + " " + creatorEmployee.LastName;
                }
                if (item.ModifierEmployeeId != null)
                {
                    var modifierEmployee = await _userManager.FindByIdAsync(item.ModifierEmployeeId);
                    mapp.ModifierEmployee = modifierEmployee.FirstName + " " + modifierEmployee.LastName;
                }
                else
                    mapp.ModifierEmployee = "";
                sectors.Add(mapp);
            }
            return sectors;
        }

        public void CatchData(string response)
        {

        }

        // GET: SectorController 
        public ActionResult Index(object response)
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
            if (data.Count() != 0)
            {
                foreach (var item in data)
                {
                    if (item.DeleteDate != null)
                    {
                        var mapp = _mapper.Map<Sector>(model);
                        mapp.CreatorEmployeeId = _userManager.GetUserId(User);
                        _repository.Insert(mapp);
                    }
                    else
                    {
                        ModelState.AddModelError("", "this name already exists");
                        return View(model);
                    }
                }
                 return RedirectToAction("Create");
            }
            else 
            {
                var mapp = _mapper.Map<Sector>(model);
                mapp.CreatorEmployeeId = _userManager.GetUserId(User);
                _repository.Insert(mapp);
                return RedirectToAction("Create");
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
            return RedirectToAction("Index","Sector");
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
