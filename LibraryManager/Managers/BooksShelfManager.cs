using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.BooksShelfModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Managers
{
    public class BooksShelfManager : Repository<BooksShelf>
    {
        private readonly IFilter<BooksShelf> _filter;
        private readonly LibraryManagerDBContext _context;
        public BooksShelfManager(LibraryManagerDBContext context,
            IFilter<BooksShelf> filter,
            UserManager<Employee> userManager) : base(context,userManager,filter)
        {
            _context = context;
            _filter = filter;
        }

        public async Task<List<BooksShelf>> FilterAsync(BooksShelfVM filter)
        {
            var entitites = _context.BooksShelves.Where(o => o.DeleteDate == null).ToList();
            List<BooksShelf> GetBySectors = filter.SectorId != 0
                ? await _filter.FilterOfEntititesByValue(filter.SectorId, "SectorId", entitites)
                : null;
            
            List<BooksShelf> GetBySections = filter.SectionId != 0
                ? await _filter.FilterOfEntititesByValue(filter.SectionId, "SectionId", entitites)
                : null;
        
            List<BooksShelf> GetByCreators = filter.CreatorId != null
                ? await _filter.FilterOfEntititesByValue(filter.CreatorId, "CreatorId", entitites)
                : null;

            List<BooksShelf> GetByModifiers = filter.ModifierId != null
                ? await _filter.FilterOfEntititesByValue(filter.ModifierId, "ModifierId", entitites)
                : null;
            
            List<BooksShelf> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterOfDate(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", entitites)
                : null;

            List<BooksShelf> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterOfDate(filter.ModifyStartDate,
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

    }
}