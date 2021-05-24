using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManager.Managers.Main
{
    public class Filter<T> : IFilter<T> where T : class
    {
        public  IEnumerable<T> IntersectAllIfEmpty(params IEnumerable<T>[] lists)
        {
            IEnumerable<T> results = null;
            var notNullList = lists.Where(l => l != null);
            lists = notNullList.Where(l => l.Any()).ToArray();
                         
            if (lists.Length > 0)
            {
                results = lists[0];

                for (int i = 1; i < lists.Length; i++)
                    results = results.Intersect(lists[i]);
            }
            else
            {
                results = new T[0];
            }

            return results;
        }
        public async Task<List<T>> FilterInBetweenDates(string dateStart,
            string dateEnd, string propertyName, List<T> elements)
        {
            await Task.Run(() => { });
            List<T> entities = new List<T>();
            if (propertyName != null && elements.Count() > 0)
            {
                if (dateStart.Count() == 0)
                    dateStart = DateTime.MinValue.ToString();
                if (dateEnd.Count() == 0)
                    dateEnd = DateTime.Now.ToString();

                DateTime start = DateTime.Parse(dateStart);
                DateTime end = DateTime.Parse(dateEnd);

                Func<T, bool> startDate = GreatThan(start, propertyName).Compile();
                Func<T, bool> endDate = LessThan(end, propertyName).Compile();
                foreach (var item in elements)
                    if (startDate(item) && endDate(item))
                        entities.Add(item);
            }

            if (entities.Count() != 0)
                return entities;
            else
                return null;
        }
        public async Task<List<T>> GetListById(string Id, string PropertyName, List<T> Elements)
        {
            return await Task.Run(() =>
            {
                List<T> Entities = new List<T>();
                if (Id.Count() != 0)
                {
                    Func<T, bool> check = ExistOrNot(Id, PropertyName).Compile();
                    foreach (var item in Elements)
                        if (check(item))
                            Entities.Add(item);
                }

                if (Entities.Count() != 0)
                    return Entities;
                else
                    return null;
            });
        }

        public Expression<Func<T, bool>> ExistOrNot(object value, string dbEntityPropertyName)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "Entity");

            MemberExpression column = Expression.PropertyOrField(pe, dbEntityPropertyName);

            BinaryExpression body =
                Expression.Equal(column, Expression.Convert(Expression.Constant(value), column.Type));

            var ExpressionTree = Expression.Lambda<Func<T, bool>>(body, new[] {pe});

            return ExpressionTree;
        }

        public Expression<Func<T, bool>> GreatThan(object value, string dbEntityPropertyName)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "Entity");

            MemberExpression column = Expression.PropertyOrField(pe, dbEntityPropertyName);

            BinaryExpression body =
                Expression.GreaterThanOrEqual(column, Expression.Convert(Expression.Constant(value), column.Type));

            var ExpressionTree = Expression.Lambda<Func<T, bool>>(body, new[] {pe});

            return ExpressionTree;
        }

        public Expression<Func<T, bool>> LessThan(object value, string dbEntityPropertyName)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "Entity");

            MemberExpression column = Expression.PropertyOrField(pe, dbEntityPropertyName);

            BinaryExpression body =
                Expression.LessThanOrEqual(column, Expression.Convert(Expression.Constant(value), column.Type));

            var expressionTree = Expression.Lambda<Func<T, bool>>(body, new[] {pe});

            return expressionTree;
        }
    }
}