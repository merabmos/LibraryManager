using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.BooksShelfModels;
using LibraryManager.Models.SectionModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Managers
{
    public class BooksShelfManager
    {
        private readonly IFilter<BooksShelf> _filter;
        private readonly LibraryManagerDBContext _context;
        private readonly SectorManager _sectorManager;
        private readonly IRepository<BooksShelf> _repository;
        public BooksShelfManager(LibraryManagerDBContext context, IFilter<BooksShelf> filter, SectorManager sectorManager, IRepository<BooksShelf> repository)
        {
            _context = context;
            _filter = filter;
            _sectorManager = sectorManager;
            _repository = repository;
        }

        public async Task<List<BooksShelf>> FilterAsync(BooksShelfVM filter)
        {
            var entitites = _context.BooksShelves.Where(o => o.DeleteDate == null).ToList();
            var sectors = _context.Sectors.ToList();
        
            List<BooksShelf> GetBySectors = filter.SectorId != 0
                ? await _filter.GetListByValue(filter.SectorId, "SectorId", entitites)
                : null;
            
            List<BooksShelf> GetBySections = filter.SectionId != 0
                ? await _filter.GetListByValue(filter.SectionId, "SectionId", entitites)
                : null;
        
            List<BooksShelf> GetByCreators = filter.CreatorId != null
                ? await _filter.GetListByValue(filter.CreatorId, "CreatorId", entitites)
                : null;

            List<BooksShelf> GetByModifiers = filter.ModifierId != null
                ? await _filter.GetListByValue(filter.ModifierId, "ModifierId", entitites)
                : null;
            
            List<BooksShelf> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterInBetweenDates(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", entitites)
                : null;

            List<BooksShelf> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterInBetweenDates(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", entitites)
                : null;

            var FilteredLists = _filter.Intersect(entitites, GetByBetweenInsertDate, GetByCreators,
                GetByModifiers,
                GetByBetweenModifyDate, GetBySections , GetBySectors);

            entitites = new List<BooksShelf>();

            foreach (var item in FilteredLists)
                entitites.Add(item);

            return entitites;
        }


        public List<SelectListItem> GetBooksShelvesSelectList()
        {
            return _repository.GetAliveEntitiesSelectList(_context.BooksShelves.Where(o => o.DeleteDate == null).ToList());
        }
        public async Task<List<BooksShelf>> FilterTableByAsync(object obj, string columnInTable)
        {
            var booksShelves = _context.BooksShelves.ToList();
            List<BooksShelf> GetBy = obj != null
                ? await _filter.GetListByValue(obj, columnInTable, booksShelves)
                : null;
            return GetBy;
        }
        //Update delete time       
        public async Task RemoveByIdAsync(object id)
        {
            var entity = await _context.BooksShelves.FindAsync(id);
            if (entity != null)
            {
                entity.DeleteDate = DateTime.Now;
                _context.BooksShelves.Update(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        
        public List<BooksShelf> FilterLists(params IEnumerable<BooksShelf>[] lists)
        {
            var FilteredSections = _filter.Intersect(lists).ToList();
            return FilteredSections;
        }
    }
}