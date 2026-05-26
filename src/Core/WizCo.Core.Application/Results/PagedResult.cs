namespace WizCo.Core.Application.Results;

public class PagedResult<T>
{
    public List<T> Items { get; }
    public int TotalPages { get; set; }
    public int TotalItems { get; }
    public int Page { get; }
    public int PageSize { get; }
    public string? Query { get; set; }

    public PagedResult(List<T> items, int totalItems, int page, int pageSize, string query = null)
    {
        Items = items;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        Query = query;
    }
}
