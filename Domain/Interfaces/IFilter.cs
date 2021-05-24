using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFilter<T> where T : class
    {
        IEnumerable<T> IntersectAllIfEmpty(params IEnumerable<T>[] lists);
        Task<List<T>> FilterInBetweenDates(string dateStart,
            string dateEnd, string propertyName, List<T> elements);
        Task<List<T>> GetListById(string Id, string PropertyName, List<T> Elements);
        Expression<Func<T, bool>> GreatThan(object value, string dbEntityPropertyName);
        Expression<Func<T, bool>> LessThan(object value, string dbEntityPropertyName);
        Expression<Func<T, bool>> ExistOrNot(object value, string dbEntityPropertyName);
    }
}
