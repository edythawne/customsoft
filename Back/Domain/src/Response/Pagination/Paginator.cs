using System.Web;

namespace Domain.Response.Pagination;

public class Paginator {
    private const long NumberPageCurrent = 1;
    
    public int CurrentPage { get; set; } = 15;
    public string? FirstPageUrl { get; set; }
    
    public string? LastPageUrl { get; set; }
    
    public string? CurrentPageUrl { get; set; }
    
    public long TotalItems { get; set; }
    
    public long ItemsPerPage { get; set; }
    
    public string? Path { get; set; }
    
    public string? Query { get; set; }
    
    public int LastPage => (int) Math.Ceiling((double) TotalItems / ItemsPerPage);

    public long DataSize { private get; set; }

    public long TotalItemsPerPageCurrent => GetTotalItemsPerPageCurrent();

    public bool HasPreviousPage => CurrentPage > NumberPageCurrent;
    
    public bool HasNextPage => CurrentPage < LastPage;

    public string GetUrl(int page) {
        var queryString = Query?.TrimStart('?');
        var query = HttpUtility.ParseQueryString(queryString!);
        query.Set("page", page.ToString());
        var url = $"{Path}?{query}";
        return url;
    }

    public long GetTotalItemsPerPageCurrent() {
        long may  = 0;
        
        if (TotalItems <= 0) {
            return may;
        }
        
        if (LastPage == NumberPageCurrent) {
            return TotalItems;
        }

        if (LastPage > NumberPageCurrent) {
            may = ((CurrentPage - NumberPageCurrent) * ItemsPerPage) + DataSize;
        }

        return may;
    }
    
}