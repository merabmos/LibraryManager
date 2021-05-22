using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManager.Managers
{
    public class Filter<T> : IFilter<T> where T : class
    {
        public async Task<List<T>> FilterInBetweenDates(string dateStart,
            string dateEnd, string PropertyName, List<T> Elements)
        {
            await Task.Run(() => { });
            List<T> Entities = new List<T>();
            if (PropertyName != null && Elements.Count() > 0)
            {
                if (dateStart.Count() == 0)
                    dateStart = DateTime.MinValue.ToString();
                if (dateEnd.Count() == 0)
                    dateEnd = DateTime.Now.ToString();

                DateTime DateStart = DateTime.Parse(dateStart);
                DateTime DateEnd = DateTime.Parse(dateEnd);

                Func<T, bool> StartDate = GreatThan(DateStart, PropertyName).Compile();
                Func<T, bool> EndDate = LessThan(DateEnd, PropertyName).Compile();
                foreach (var item in Elements)
                    if (StartDate(item) && EndDate(item))
                        Entities.Add(item);
            }
            return Entities;
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
                return Entities;
            });
        }

        public Expression<Func<T, bool>> ExistOrNot(object value, string DB_EntityPropertyName)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "Entity");

            MemberExpression column = Expression.PropertyOrField(pe, DB_EntityPropertyName);

            BinaryExpression body = Expression.Equal(column, Expression.Convert(Expression.Constant(value), column.Type));

            var ExpressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });

            return ExpressionTree;
        }

        public Expression<Func<T, bool>> GreatThan(object value ,string DB_EntityDatePropertyName)
        {
            ParameterExpression pe = Expression.Parameter(typeof(T), "Entity");

            MemberExpression column = Expression.PropertyOrField(pe, DB_EntityDatePropertyName);

            BinaryExpression body = Expression.GreaterThanOrEqual(column, Expression.Convert(Expression.Constant(value), column.Type));

            var ExpressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });

            return ExpressionTree;
        }

        public Expression<Func<T, bool>> LessThan(object value, string DB_EntityDatePropertyName)
        {

            ParameterExpression pe = Expression.Parameter(typeof(T), "Entity");

            MemberExpression column = Expression.PropertyOrField(pe, DB_EntityDatePropertyName);

            BinaryExpression body = Expression.LessThanOrEqual(column, Expression.Convert(Expression.Constant(value), column.Type));

            var ExpressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });

            return ExpressionTree;
        }

    }
}
