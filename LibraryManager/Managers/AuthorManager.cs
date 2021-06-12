using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Domain.Entities;
using Domain.Interfaces;
using LibraryManager.Managers.Main;
using LibraryManager.Models.AuthorModels;
using LibraryManager.Models.GenreModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.Managers
{
    public class AuthorManager : Repository<Author>
    {
        private readonly IFilter<Author> _filter;
        private readonly LibraryManagerDBContext _context;

        public AuthorManager(LibraryManagerDBContext context,
            IFilter<Author> filter,
            UserManager<Employee> _userManager) : base(context,_userManager,filter)
        {
            _context = context;
            _filter = filter;
        }

        public async Task<List<Author>> FilterAsync(AuthorVM filter)
        {
            var authors = _context.Authors.Where(o => o.DeleteDate == null).ToList();
            List<Author> GetByCreators = filter.CreatorId != null
                ? await _filter.FilterOfEntititesByValue(filter.CreatorId, "CreatorId", authors)
                : null;
            List<Author> GetByModifiers = filter.ModifierId != null
                ? await _filter.FilterOfEntititesByValue(filter.ModifierId, "ModifierId", authors)
                : null;

            List<Author> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterOfDate(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", authors)
                : null;

            List<Author> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterOfDate(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", authors)
                : null;

            var FilteredAuthors = _filter.Intersect(authors, GetByBetweenInsertDate, GetByCreators,
                GetByModifiers,
                GetByBetweenModifyDate).ToList();

            return FilteredAuthors;
        }
    }
}