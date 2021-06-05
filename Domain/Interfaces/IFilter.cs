using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFilter<T> where T : class
    {
        IEnumerable<T> Intersect(params IEnumerable<T>[] lists);
        Task<List<T>> FilterInBetweenDates(string dateStart,
            string dateEnd, string propertyName, List<T> elements);
        Task<List<T>> GetListByValue(object value, string PropertyName, List<T> Elements);
    }
}
