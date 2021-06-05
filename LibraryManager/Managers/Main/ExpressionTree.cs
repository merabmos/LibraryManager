using System;
using System.Linq.Expressions;
using Domain.Interfaces;

namespace LibraryManager.Managers.Main
{
    public class ExpressionTree<T> : IExpressionTree<T>  where T : class
    {
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

            var expressionTree = Expression.Lambda<Func<T, bool>>(body, new[] {pe});

            return expressionTree;
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