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
        Task<List<T>> FilterInBetweenDates(string dateStart,
            string dateEnd, string PropertyName, List<T> Entities);
        Task<List<T>> GetListById(string Id, string PropertyName, List<T> Elements);
        Expression<Func<T, bool>> GreatThan(object value, string DB_EntityDatePropertyName);
        Expression<Func<T, bool>> LessThan(object value, string DB_EntityDatePropertyName);
        Expression<Func<T, bool>> ExistOrNot(object value, string DB_EntityPropertyName);
    }
}
