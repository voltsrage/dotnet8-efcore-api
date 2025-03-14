using EFCore.API.Models.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

            // Uses reflection to get the class type
            // This retrieves metadata about the generic type T at runtime
            // For example, if T is the Hotel class, this gets type information about Hotel
            var type = typeof(T);

            // Get all public instance properties of the type
            // These are the properties that can be accessed when sorting
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // Find the property that matches the requested sort column name (case-insensitive)
            // This enables sorting by any property regardless of case (e.g., "name", "Name", or "NAME")
            var property = properties.FirstOrDefault(p => p.Name.Equals(sortColumn, StringComparison.OrdinalIgnoreCase));

            // If the property doesn't exist, return the unsorted query
            // This gracefully handles invalid sort column names
            if (property == null)
                return query;  // Property not found, return unsorted query

            // Create a parameter expression that represents the input object in the lambda
            // This creates a parameter named "x" of type T (e.g., Hotel)
            var parameter = Expression.Parameter(type, "x");

            // Create an expression that accesses the property on the parameter
            // This represents "x.PropertyName" (e.g., "x.Name", "x.Price", etc.)
            var propertyAccess = Expression.Property(parameter, property);

            // Create a lambda expression that takes an object and returns the property value
            // This builds the equivalent of "x => x.PropertyName"
            var lambda = Expression.Lambda(propertyAccess, parameter);

            // Determine which sort method to use based on the requested sort direction
            // "OrderBy" for ascending, "OrderByDescending" for descending
            var methodName = isAscending ? "OrderBy" : "OrderByDescending";

            // Find the appropriate sort method from the Queryable static methods
            // This looks for OrderBy/OrderByDescending with the right signature:
            // - Must be a generic method definition
            // - Must take exactly 2 parameters (the query and the sort selector)
            var method = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
                .Where(m => m.GetParameters().Length == 2)
                .FirstOrDefault();

            // Make the generic method specific to our type and property type
            // For example: OrderBy<Hotel, string> for sorting Hotels by Name
            var genericMethod = method.MakeGenericMethod(type, property.PropertyType);

            // Invoke the sort method on the query with our lambda expression
            // This executes the equivalent of: query.OrderBy(x => x.PropertyName)
            // The result is returned as IQueryable<T>
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda });
        }

        /// <summary>
        /// Apply string search to a query
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="query">The query to filter</param>
        /// <param name="searchText">Text to search for</param>
        /// <param name="propertyPaths">Names of string properties to search in</param>
        /// <returns>The filtered query</returns>
        public static IQueryable<T> ApplySearch<T>(
            this IQueryable<T> query,
            string searchText,
            params string[] propertyPaths)
        {
            // Check again if searchTerm or properties to be search are missing
            if(string.IsNullOrWhiteSpace(searchText) || propertyPaths.Length == 0)
                return query;

            // Uses reflection to get the class type
            // This retrieves metadata about the generic type T at runtime
            // For example, if T is the Hotel class, this gets type information about Hotel
            var type = typeof(T);

            // Create a parameter expression that represents the input object
            // This creates a variable named "x" of type T that will be used in the expression
            // It's equivalent to the "item" in a lambda expression like: items.Where(x => ...)
            var parameter = Expression.Parameter(type, "x");

            // Create a list to hold all the individual search expressions
            // Each expression will check if a specific property contains the search text
            // These expressions will later be combined with OR operators
            var searchExpressions = new List<Expression>();

            foreach(var propertyPath in propertyPaths)
            {
                // Split the property path string by dots to handle navigation properties
                // For example: "Hotel.City" becomes ["Hotel", "City"]
                // This allows us to traverse object hierarchies (navigation properties in EF)
                var parts = propertyPath.Split('.');

                // Start with the parameter expression (represents the root entity 'x')
                // This will be built upon for each level of property access
                Expression propertyAccess = parameter;

                // Track the current type as we navigate through properties
                // Initially this is the entity type (T), but changes as we traverse properties
                Type currentType = type;

                // Will hold the PropertyInfo for the last property in the chain
                // This is needed later to check if it's a string property
                PropertyInfo property = null;

                // Flag to track if the entire property path is valid
                // Will be set to false if any property in the chain doesn't exist
                bool isValidPath = true;

                foreach(var part in parts)
                {
                    // Use reflection to find the property on the current type
                    // BindingFlags ensure we find public instance properties regardless of case sensitivity
                    property = currentType.GetProperty(part, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    // If property doesn't exist, mark the path as invalid and stop building
                    if (property == null)
                    {
                        isValidPath = false;
                        break;
                    }

                    // Build the property access expression for this level
                    // If we already have x.Hotel, this adds .City to make x.Hotel.City
                    propertyAccess = Expression.Property(propertyAccess, property);

                    // Update the current type to the property's type for the next iteration
                    // This is critical for correctly navigating nested properties
                    currentType = property.PropertyType;
                }

                // Skip this property path if:
                // - Any part of the path was invalid (property didn't exist)
                // - The last property is null (should never happen if isValidPath is true)
                // - The last property is not a string (we can only do Contains() on strings)
                if (!isValidPath || property == null || property.PropertyType != typeof(string))
                    continue;

                // Create a constant expression for the search text
                var searchValue = Expression.Constant(searchText, typeof(string));

                // Get the Contains method from the String class
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                // Create the method call expression for the final property
                // Example: x.Hotel.City.Contains("searchText")
                var containsExpression = Expression.Call(propertyAccess, containsMethod, searchValue);


                // Start with the Contains expression as our base expression
                // We'll add null checks to this
                Expression nullCheckExpression = containsExpression;

                // Reset to build the null-check expressions
                // We need to rebuild the property access chain to add null checks at each level
                propertyAccess = parameter;
                currentType = type;

                // Second pass: add null checks for each level in the property chain
                foreach (var part in parts)
                {
                    // Get the property again (we already validated it exists)
                    property = currentType.GetProperty(part, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    // Build up the property access expression again, one level at a time
                    propertyAccess = Expression.Property(propertyAccess, property);

                    // Add null checks for reference type navigation properties (not for value types or the final property)
                    // Value types (int, decimal, etc.) can't be null, so no need to check them
                    // We'll handle the final property's null check separately below
                    if (part != parts.Last() && !property.PropertyType.IsValueType)
                    {
                        // Create expression: x.NavigationProperty != null
                        var notNullExpression = Expression.NotEqual(propertyAccess, Expression.Constant(null, property.PropertyType));

                        // Combine with existing expression using AND
                        // This ensures we only proceed if the navigation property is not null
                        // Example: (x.Hotel != null) AND (rest of expression)
                        nullCheckExpression = Expression.AndAlso(notNullExpression, nullCheckExpression);
                    }

                    // Update current type for the next iteration
                    currentType = property.PropertyType;
                }

                // Add a final null check for the string property itself
                // This prevents calling Contains() on a null string value
                // Example: x.Hotel.City != null
                var finalNotNullExpression = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));

                // Combine the final null check with the rest of the expression
                // Complete example: (x.Hotel != null) AND (x.Hotel.City != null) AND (x.Hotel.City.Contains("searchText"))
                nullCheckExpression = Expression.AndAlso(finalNotNullExpression, nullCheckExpression);

                // Add this completed expression to our collection of search conditions
                searchExpressions.Add(nullCheckExpression);

            }

            if(searchExpressions.Count == 0)
                return query;

            // Combine all property search expressions with OR operations
            // This creates a single expression that checks if ANY property contains the search text
            // Example: (x.Name != null && x.Name.Contains("searchText")) OR 
            //          (x.Description != null && x.Description.Contains("searchText")) OR ...
            var combinedSearchExpression = searchExpressions.Aggregate(Expression.OrElse);

            // Convert the combined expression into a lambda expression
            // This transforms our expression tree into a delegate that can be executed
            // The result is equivalent to: x => (x.Name != null && x.Name.Contains("searchText")) OR ...
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
