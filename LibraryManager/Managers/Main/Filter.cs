using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Managers.Main
{
    public class Filter<T> : ExpressionTree<T>, IFilter<T> where T : class
    {
        private readonly DbSet<T> _table;
        public Filter(LibraryManagerDBContext context)
        {
            _table = context.Set<T>();
        }
        
        public IEnumerable<T> Intersect(params IEnumerable<T>[] lists)
        {
            IEnumerable<T> results = null;

            lists = lists.Where(l => l != null).ToArray();

            if (lists.Any())
                if (lists.Length > 0)
                {
                    results = lists[0];

                    for (int i = 1; i < lists.Length; i++)
                        results = results.Intersect(lists[i]);
                }
                else
                    results = new T[0];

            return results;
        }
        
   
        public async Task<List<T>> FilterOfDate(string dateStart,
            string dateEnd, string propertyName, List<T> elements)
        {
            await Task.Run(() => { });
            List<T> entities = new List<T>();

            if (propertyName != null && elements.Any())
            {
                dateStart ??= DateTime.MinValue.ToString(CultureInfo.InvariantCulture); 
                dateEnd ??= DateTime.Now.ToString(CultureInfo.InvariantCulture);

                DateTime start = DateTime.Parse(dateStart);
                DateTime end = DateTime.Parse(dateEnd);

                Func<T, bool> startDate = GreatThan(start, propertyName).Compile();
                Func<T, bool> endDate = LessThan(end, propertyName).Compile();
                foreach (var item in elements)
                    if (startDate(item) && endDate(item))
                        entities.Add(item);
            }
            return entities;
        }

        public async Task<List<T>> FilterOfEntititesByValue(object value, string PropertyName, List<T> Elements = null)
        {
            Elements ??= _table.ToList();
            return await Task.Run(() =>
            {
                List<T> entities = new List<T>();
                if (value != null)
                {
                    Func<T, bool> check = ExistOrNot(value, PropertyName).Compile();
                    foreach (var item in Elements)
                        if (check(item))
                            entities.Add(item);
                }
                return entities;
            });
        }
    }
}