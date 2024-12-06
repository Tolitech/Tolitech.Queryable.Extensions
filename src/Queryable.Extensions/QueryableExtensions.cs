using System.Linq.Expressions;
using System.Reflection;

namespace Tolitech.Queryable.Extensions;

/// <summary>
/// Provides extension methods for querying data dynamically and performing pagination.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Orders the elements of a sequence dynamically based on the specified sorting criteria.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="query">The IQueryable to apply sorting to.</param>
    /// <param name="orderByString">A string specifying the sorting criteria.</param>
    /// <returns>An IQueryable with elements sorted based on the specified criteria.</returns>
    public static IQueryable<T> OrderByExpression<T>(this IQueryable<T> query, string orderByString)
    {
        if (string.IsNullOrWhiteSpace(orderByString))
        {
            return query;
        }

        string[] orderByArray = orderByString.Split(',');

        bool firstOrderBy = true;
        foreach (string orderBy in orderByArray)
        {
            string[] parts = orderBy.Trim().Split(':');
            string propertyName = parts[0];
            bool ascending = true;

            if (parts.Length > 1 && parts[1].StartsWith("desc", StringComparison.InvariantCultureIgnoreCase))
            {
                ascending = false;
            }

            query = ApplyOrder(query, propertyName, ascending, firstOrderBy);

            if (firstOrderBy)
            {
                firstOrderBy = false;
            }
        }

        return query;
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The IQueryable to paginate.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns>An IQueryable containing elements of the specified page.</returns>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;

        return source
            .Skip(skip)
            .Take(pageSize);
    }

    /// <summary>
    /// Applies sorting to the elements of a sequence based on the specified criteria.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="query">The IQueryable to apply sorting to.</param>
    /// <param name="propertyName">The name of the property to sort by.</param>
    /// <param name="ascending">True if sorting should be in ascending order; otherwise, false.</param>
    /// <param name="firstOrderBy">True if this is the first ordering operation; otherwise, false.</param>
    /// <returns>An IQueryable with elements sorted based on the specified criteria.</returns>
    private static IQueryable<T> ApplyOrder<T>(IQueryable<T> query, string propertyName, bool ascending, bool firstOrderBy)
    {
        Type entityType = typeof(T);
        ParameterExpression parameter = Expression.Parameter(entityType, "x");
        Expression propertyAccess = parameter;

        foreach (string property in propertyName.Split('.'))
        {
            propertyAccess = Expression.PropertyOrField(propertyAccess, property);
        }

        LambdaExpression orderByExp = Expression.Lambda(propertyAccess, parameter);

        string methodName = ascending ? "OrderBy" : "OrderByDescending";

        if (!firstOrderBy)
        {
            methodName = ascending ? "ThenBy" : "ThenByDescending";
        }

        MethodInfo orderByMethod = typeof(System.Linq.Queryable).GetMethods().Single(
            method => method.Name == methodName &&
                      method.IsGenericMethodDefinition &&
                      method.GetGenericArguments().Length == 2 &&
                      method.GetParameters().Length == 2)
            .MakeGenericMethod(entityType, propertyAccess.Type);

        return (IQueryable<T>)orderByMethod.Invoke(null, [query, orderByExp])!;
    }
}