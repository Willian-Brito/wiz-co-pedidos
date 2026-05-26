using WizCo.Core.Domain.Common;

namespace WizCo.Core.Domain.Common;

public static class EnumerableExtensions
{
    public static PagedResult<T> ToPagedList<T>(
        this IEnumerable<T> list,
        int pageNumber = 1,
        int pageSize = 10,
        string search = null
    )
    {
        var count = list.Count();
        var items = list.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>(items, count, pageNumber, pageSize, search);
    }
}