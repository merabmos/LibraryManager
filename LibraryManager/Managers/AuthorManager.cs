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
        private readonly IRepository<Author> _repository;

        public AuthorManager(LibraryManagerDBContext context,
            IFilter<Author> filter,
            IRepository<Author> repository,
            UserManager<Employee> _userManager) : base(context,_userManager)
        {
            _context = context;
            _filter = filter;
            _repository = repository;
        }

        public async Task<List<Author>> FilterAsync(AuthorVM filter)
        {
            var authors = _context.Authors.Where(o => o.DeleteDate == null).ToList();
            List<Author> GetByCreators = filter.CreatorId != null
                ? await _filter.GetListByValue(filter.CreatorId, "CreatorId", authors)
                : null;
            List<Author> GetByModifiers = filter.ModifierId != null
                ? await _filter.GetListByValue(filter.ModifierId, "ModifierId", authors)
                : null;

            List<Author> GetByBetweenInsertDate = filter.InsertStartDate != null || filter.InsertEndDate != null
                ? await _filter.FilterInBetweenDates(filter.InsertStartDate,
                    filter.InsertEndDate, "InsertDate", authors)
                : null;

            List<Author> GetByBetweenModifyDate = filter.ModifyStartDate != null || filter.ModifyEndDate != null
                ? await _filter.FilterInBetweenDates(filter.ModifyStartDate,
                    filter.ModifyEndDate, "ModifyDate", authors)
                : null;

            var FilteredAuthors = _filter.Intersect(authors, GetByBetweenInsertDate, GetByCreators,
                GetByModifiers,
                GetByBetweenModifyDate).ToList();

            return FilteredAuthors;
        }
        
        public async Task<List<Author>> FilterOfTableByAsync(object obj, string columnInTable)
        {
            var Entities = _context.Authors.ToList();
            List<Author> GetBySectors = obj != null
                ? await _filter.GetListByValue(obj, columnInTable, Entities)
                : null;
            return GetBySectors;
        }
        public List<Author> FilterLists(params IEnumerable<Author>[] lists)
        {
            var FilteredSections = _filter.Intersect(lists).ToList();
            return FilteredSections;
        }
    }
}