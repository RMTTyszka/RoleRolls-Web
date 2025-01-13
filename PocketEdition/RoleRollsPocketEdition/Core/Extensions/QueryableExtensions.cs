using System.Linq.Expressions;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition.Core.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition
            ? query.Where(predicate)
            : query;
    }   
    public static IQueryable<T> PageBy<T>(this IQueryable<T> query, PagedRequestInput input)
    {
        return query.Skip(input.SkipCount).Take(input.MaxResultCount);
    }
    
}