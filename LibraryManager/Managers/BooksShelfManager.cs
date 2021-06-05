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

namespace LibraryManager.Managers
{
    public class BooksShelfManager
    {
        private readonly IFilter<BooksShelf> _filter;
        private readonly LibraryManagerDBContext _context;
        private readonly SectorManager _sectorManager;   
        public BooksShelfManager(LibraryManagerDBContext context, IFilter<BooksShelf> filter, SectorManager sectorManager)
        {
            _context = context;
            _filter = filter;
            _sectorManager = sectorManager;
        }

        public async Task<List<BooksShelf>> FilterAsync(BooksShelfVM filter)
        {
            var entitites = _context.BooksShelves.Where(o => o.DeleteDate == null).ToList();
            var sectors = _context.Sectors.ToList();
       
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
                GetByBetweenModifyDate, GetBySections);

            entitites = new List<BooksShelf>();

            foreach (var item in FilteredLists)
                entitites.Add(item);

            return entitites;
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
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                section.DeleteDate = DateTime.Now;
                _context.Sections.Update(section);
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