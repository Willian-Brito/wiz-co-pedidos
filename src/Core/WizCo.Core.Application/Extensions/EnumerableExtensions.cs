using WizCo.Core.Application.Results;

namespace WizCo.Core.Application.Extensions;

public static class EnumerableExtensions
{
    public static PagedResult<T> ToPagedList<T>(
        this IEnumerable<T> list,
        int pageNumber = 1,
        int pageSize = 10,
        string query = null
    )
    {
        var count = list.Count();
        var items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>(items, count, pageNumber, pageSize, query);
    }
}