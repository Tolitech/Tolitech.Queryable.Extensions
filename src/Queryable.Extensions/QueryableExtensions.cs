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
        ArgumentNullException.ThrowIfNull(query);

        if (string.IsNullOrWhiteSpace(orderByString))
        {
            return query;
        }

        string[] orderByArray = orderByString.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        bool firstOrder = true;

        foreach (string orderSegment in orderByArray)
        {
            string[] parts = orderSegment.Split(':', StringSplitOptions.TrimEntries);
            string propertyPath = parts[0];
            bool ascending = parts.Length == 1 || !parts[1].StartsWith("desc", StringComparison.OrdinalIgnoreCase);

            LambdaExpression keySelector = BuildKeySelector<T>(propertyPath);

            query = ApplyOrdering(query, keySelector, ascending, firstOrder);
            firstOrder = false;
        }

        return query;
    }

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="query">The IQueryable to paginate.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns>An IQueryable containing elements of the specified page.</returns>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        int skip = Math.Max(0, (pageNumber - 1) * pageSize);
        return query.Skip(skip).Take(pageSize);
    }

    private static LambdaExpression BuildKeySelector<T>(string propertyPath)
    {
        Type type = typeof(T);
        ParameterExpression param = Expression.Parameter(type, "x");
        Expression body = param;

        foreach (string property in propertyPath.Split('.'))
        {
            PropertyInfo? propInfo = body.Type.GetProperty(
                property,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ?? throw new ArgumentException($"Property '{property}' not found on type '{body.Type.Name}'");

            body = Expression.Property(body, propInfo);
        }

        return Expression.Lambda(body, param);
    }

    private static IQueryable<T> ApplyOrdering<T>(
        IQueryable<T> source,
        LambdaExpression keySelector,
        bool ascending,
        bool isFirstOrder)
    {
        string methodName = GetOrderingMethodName(ascending, isFirstOrder);

        MethodInfo method = typeof(System.Linq.Queryable)
            .GetMethods()
            .First(m => m.Name == methodName
                     && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), keySelector.Body.Type);

        return (IQueryable<T>)method.Invoke(null, [source, keySelector])!;
    }

    private static string GetOrderingMethodName(bool ascending, bool isFirstOrder)
    {
        if (isFirstOrder)
        {
            return ascending ? nameof(System.Linq.Queryable.OrderBy) : nameof(System.Linq.Queryable.OrderByDescending);
        }

        return ascending ? nameof(System.Linq.Queryable.ThenBy) : nameof(System.Linq.Queryable.ThenByDescending);
    }
}