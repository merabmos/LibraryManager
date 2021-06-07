using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Database;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Managers.Main
{
    public class Manager<T> where T : class
    {
        private readonly IFilter<T> _filter;
        private readonly IRepository<T> _repository;
        private readonly IExpressionTree<T> _expressionTree;
        private readonly DbSet<T> table = null;

        public Manager(IFilter<T> filter, IRepository<T> repository, IExpressionTree<T> expressionTree,
            LibraryManagerDBContext context)
        {
            _filter = filter;
            _repository = repository;
            _expressionTree = expressionTree;
            table = context.Set<T>();
        }


        public List<T> FilterOfLists(params IEnumerable<T>[] lists)
        {
            var FilteredSections = _filter.Intersect(lists).ToList();
            return FilteredSections;
        }

        public async Task<List<T>> FilterOfTableByAsync(object obj, string columnInTable)
        {
            var Entities = table.ToList();
            List<T> GetBySectors = obj != null
                ? await _filter.GetListByValue(obj, columnInTable, Entities)
                : null;
            return GetBySectors;
        }
        
        
        public async Task<T> GetEntityById(object id)
        {
            return await table.FindAsync(id);
        }

        public List<SelectListItem> GetEntitiesSelectList(List<T> list)
        {
            return _repository.GetAliveEntitiesSelectList(list);
        }

        public async Task RemoveByIdAsync(object id)
        {
            var entity = await table.FindAsync(id);
            if (entity != null)
            {
                entity.GetType().GetProperty("DeleteDate")?.SetValue(entity,DateTime.Now);;
                 _repository.Update(entity);
            }
        }
    }
}