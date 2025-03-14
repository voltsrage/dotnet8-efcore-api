using EFCore.API.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace EFCore.API.Extensions
{
    /// <summary>
    /// Helper class for pagination and dynamic querying with Entity Framework
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies pagination parameters to an IQueryable
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="query">The IQueryable to paginate</param>
        /// <param name="request">Pagination parameters</param>
        /// <returns>A tuple containing the paginated query and the total count</returns>
        public static async Task<(IQueryable<T> Query, int TotalCount)> ApplyPaginationAsync<T>(
            this IQueryable<T> query, PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply sorting
            if (request.HasSorting)
            {
                query = ApplySorting(query, request.SortColumn, request.IsAscending);
            }

            query = query.Skip(request.Skip).Take(request.PageSize);

            return (query, totalCount);
        }

        /// <summary>
        /// Creates a paginated result from an IQueryable
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="query">The IQueryable to paginate</param>
        /// <param name="request">Pagination parameters</param>
        /// <returns>Paginated result</returns>
        public static async Task<PaginatedResult<T>> ToPaginatedResultAsync<T>(
            this IQueryable<T> query,
            PaginationRequest request)
        {
            var (paginatedQuery, totalCount) = await query.ApplyPaginationAsync(request);

            var items = await Task.FromResult(paginatedQuery.ToList());

            return new PaginatedResult<T>(items, totalCount, request.Page, request.PageSize);
        }

        private static IQueryable<T> ApplySorting<T>
            (IQueryable<T> query,
            string? sortColumn,
            bool isAscending)
        {
            if(string.IsNullOrWhiteSpace(sortColumn))
                return query;

            // Get Entity Type
            var type = typeof(T);

            // Try to find the property based on case-insensitive match
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var property = properties.FirstOrDefault(p => p.Name.Equals(sortColumn, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                return query;  // Property not found, return unsorted query

            // Create a parameter expression for the entity
            var parameter = Expression.Parameter(type, "x");

            // Create am expression for the property
            var propertyAccess = Expression.Property(parameter, property);

            // Create a lambda expression for the sorting
            var lambda = Expression.Lambda(propertyAccess, parameter);

            // Get the appropriate method (OrderBy or OrderByDescending)
            var methodName = isAscending ? "OrderBy" : "OrderByDescending";
            var method = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                .Where(m => m.GetParameters().Length == 2)
                .FirstOrDefault();

            // Make the method generic with appropriate types
            var genericMethod = method.MakeGenericMethod(type, property.PropertyType);

            // Apply the sorting to the query
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda });
        }

        /// <summary>
        /// Apply string search to a query
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="query">The query to filter</param>
        /// <param name="searchText">Text to search for</param>
        /// <param name="propertyNames">Names of string properties to search in</param>
        /// <returns>The filtered query</returns>
        public static IQueryable<T> ApplySearch<T>(
            this IQueryable<T> query,
            string searchText,
            params string[] propertyNames)
        {
            if(string.IsNullOrWhiteSpace(searchText) || propertyNames.Length == 0)
                return query;

            var type = typeof(T);
            var parameter = Expression.Parameter(type, "x");

            // Build individual property search expressions
            var searchExpressions = new List<Expression>();

            foreach(var propertyName in propertyNames)
            {
                var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (property != null && property.PropertyType == typeof(string))
                {
                    var propertyAccess = Expression.Property(parameter, property);
                    var searchValue = Expression.Constant(searchText, typeof(string));

                    // Create x.Property.Contains(searchText)
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var containsExpressions = Expression.Constant(searchText, typeof(string));

                    // Add null check: x.Property != null && x.Property.Contains(searchText)
                    var notNullExpression = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
                    var combinedExpression = Expression.AndAlso(notNullExpression, containsExpressions);

                    searchExpressions.Add(combinedExpression);
                }
            }

            if(searchExpressions.Count == 0)
                return query;

            // Combine all search expressions with OR
            var combinedSearchExpression = searchExpressions.Aggregate(Expression.OrElse);
            var lambda = Expression.Lambda<Func<T, bool>>(combinedSearchExpression, parameter);

            // Apply the filter
            return query.Where(lambda);
        }

        /// <summary>
        /// Apply dynamic filtering based on a dictionary of conditions
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="query">The query to filter</param>
        /// <param name="filters">Dictionary of property names and filter values</param>
        /// <returns>The filtered query</returns>
        public static IQueryable<T> ApplyFilters<T>(
            this IQueryable<T> query,
            Dictionary<string, string> filters)
        {
            if(filters == null || filters.Count == 0)
                return query;

            var type = typeof(T);

            var parameter = Expression.Parameter(type, "x");

            foreach(var filter in filters)
            {
                string propertyName = filter.Key; 
                string filterValue = filter.Value;

                // Handle special operators in property name (e.g. "price__gte")
                string op = "eq"; // Default to equals

                if (propertyName.Contains("__"))
                {
                    var parts = propertyName.Split("__");
                    propertyName = parts[0];
                    op = parts[1].ToLowerInvariant();
                }

                var property = type.GetProperty(propertyName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if(property != null)
                {
                    var propertyAccess = Expression.Property(parameter, property);
                    Expression filterExpression = null;

                    if (property.PropertyType == typeof(string)) 
                    {
                        // String comparisons
                        var stringValue = Expression.Constant(filterValue, typeof(string));

                        filterExpression = op switch
                        {
                            "contains" => CreateStringContainsExpression(propertyAccess, stringValue),
                            "startswith" => CreateStringStartsWithExpression(propertyAccess, stringValue),
                            "endswith" => CreateStringEndsWithExpression(propertyAccess, stringValue),
                            _ => Expression.Equal(propertyAccess, stringValue) // Default to equals
                        };
                    }
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        // Integer comparisons
                        if (int.TryParse(filterValue, out var intValue))
                        {
                            var constantValue = Expression.Constant(intValue, property.PropertyType);

                            filterExpression = op switch
                            {
                                "gt" => Expression.GreaterThan(propertyAccess, constantValue),
                                "gte" => Expression.GreaterThanOrEqual(propertyAccess, constantValue),
                                "lt" => Expression.LessThan(propertyAccess, constantValue),
                                "lte" => Expression.LessThanOrEqual(propertyAccess, constantValue),
                                _ => Expression.Equal(propertyAccess, constantValue) // Default to equals
                            };
                        }
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        // Decimal comparisons
                        if (decimal.TryParse(filterValue, out var decimalValue))
                        {
                            var constantValue = Expression.Constant(decimalValue, property.PropertyType);

                            filterExpression = op switch
                            {
                                "gt" => Expression.GreaterThan(propertyAccess, constantValue),
                                "gte" => Expression.GreaterThanOrEqual(propertyAccess, constantValue),
                                "lt" => Expression.LessThan(propertyAccess, constantValue),
                                "lte" => Expression.LessThanOrEqual(propertyAccess, constantValue),
                                _ => Expression.Equal(propertyAccess, constantValue) // Default to equals
                            };
                        }
                    }
                    else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                    {
                        // Boolean comparisons
                        if (bool.TryParse(filterValue, out var boolValue))
                        {
                            var constantValue = Expression.Constant(boolValue, property.PropertyType);
                            filterExpression = Expression.Equal(propertyAccess, constantValue);
                        }
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        // DateTime comparisons
                        if (DateTime.TryParse(filterValue, out var dateValue))
                        {
                            var constantValue = Expression.Constant(dateValue, property.PropertyType);

                            filterExpression = op switch
                            {
                                "gt" => Expression.GreaterThan(propertyAccess, constantValue),
                                "gte" => Expression.GreaterThanOrEqual(propertyAccess, constantValue),
                                "lt" => Expression.LessThan(propertyAccess, constantValue),
                                "lte" => Expression.LessThanOrEqual(propertyAccess, constantValue),
                                _ => Expression.Equal(propertyAccess, constantValue) // Default to equals
                            };
                        }
                    }

                    if (filterExpression != null)
                    {
                        var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                        query = query.Where(lambda);
                    }
                }
            }

            return query;
        }

        private static Expression CreateStringContainsExpression(Expression propertyAccess, Expression stringValue)
        {
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var notNullExpression = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            var containsExpression = Expression.Call(propertyAccess, containsMethod, stringValue);
            return Expression.AndAlso(notNullExpression, containsExpression);
        }

        private static Expression CreateStringStartsWithExpression(Expression propertyAccess, Expression stringValue)
        {
            var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var notNullExpression = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            var startsWithExpression = Expression.Call(propertyAccess, startsWithMethod, stringValue);
            return Expression.AndAlso(notNullExpression, startsWithExpression);
        }

        private static Expression CreateStringEndsWithExpression(Expression propertyAccess, Expression stringValue)
        {
            var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
            var notNullExpression = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            var endsWithExpression = Expression.Call(propertyAccess, endsWithMethod, stringValue);
            return Expression.AndAlso(notNullExpression, endsWithExpression);
        }
    }
}
