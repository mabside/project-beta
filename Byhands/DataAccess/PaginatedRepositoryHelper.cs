using System.Reflection;
using Byhands.Domain;

namespace Byhands.DataAccess;

public static class PaginatedRepositoryHelper
{
    private static readonly Dictionary<string, IEnumerable<string>> Cache = new();

    private static IEnumerable<string> GetPropertyName(Type type)
    {
        var declaredClassProperties = type.GetTypeInfo().DeclaredProperties.Select(x => x.Name);
        return new List<string>(declaredClassProperties)
        {
            nameof(BaseEntity<int>.CreatedOn),
            nameof(BaseEntity<int>.ModifiedOn)
        };
    }

    public static bool IsValidSortColumn<TEntity>(string? columnName)
        where TEntity : class
    {
        if (string.IsNullOrWhiteSpace(columnName))
            return false;

        return GetClassProperties<TEntity>().Any(x => string.Equals(x, columnName, StringComparison.OrdinalIgnoreCase));
    }

    public static IEnumerable<string> GetClassProperties<TEntity>() where TEntity : class
        => GetClassProperties(typeof(TEntity));

    public static IEnumerable<string> GetClassProperties(Type type)
    {
        var key = type.FullName ?? string.Empty;

        if (!Cache.TryGetValue(key, out var properties))
        {
            properties = GetPropertyName(type);
            Cache[key] = properties;
        }
        return properties;
    }
}