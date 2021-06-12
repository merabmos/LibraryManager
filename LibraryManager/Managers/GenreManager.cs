using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Domain.Interfaces;
using Domain.Entities;
using System.Linq;
using LibraryManager.Managers.Main;
using LibraryManager.Models.GenreModels;
using LibraryManager.Models.SectorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManager.Managers
{
    public class GenreManager : Repository<Genre>
    {
        private readonly IFilter<Genre> _filter;
        private readonly LibraryManagerDBContext _context;

        public GenreManager(LibraryManagerDBContext context,
            IFilter<Genre> filter,
            IRepository<Genre> repository,
            UserManager<Employee> _userManager) : base(context,_userManager,filter)
        {
            _context = context;
            _filter = filter;
        }

        public async Task<List<Genre>> FilterAsync(GenreVM filter)
        {
            var genres = _context.Genres.Where(o => o.DeleteDate == null).ToList();
            List<Genre> GetByCreators = filter.CreatorId != null
                ? await _filter.FilterOfEntititesByValue(filter.CreatorId, "CreatorId", genres)
                : null;
            List<Genre> GetByModifiers = filter.ModifierId != null
                ? await _filter.FilterOfEntititesByValue(filter.ModifierId, "ModifierId", genres)
                : null;

            List<Genre> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterOfDate(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", genres)
                : null;

            List<Genre> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterOfDate(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", genres)
                : null;

            var FilteredGenres = _filter.Intersect(genres, GetByBetweenInsertDate, GetByCreators,
                GetByModifiers,
                GetByBetweenModifyDate).ToList();

            return FilteredGenres;
        }
    }
}