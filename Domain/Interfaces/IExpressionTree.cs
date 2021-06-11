using System;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IExpressionTree<T> where T : class
    {
        Expression<Func<T, bool>> GreatThan(object value, string dbEntityPropertyName);
        Expression<Func<T, bool>> LessThan(object value, string dbEntityPropertyName);
        Expression<Func<T, bool>> ExistOrNot(object value, string dbEntityPropertyName);
    }
}