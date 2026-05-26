namespace WizCo.Core.Domain.Common;

public class PagedResult<T>
{
    public List<T> Items { get; }
    public int TotalPages { get; set; }
    public int TotalItems { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public string? Search { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public PagedResult(List<T> items, int totalItems, int pageNumber, int pageSize, string search = null)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        Search = search;
    }
}
