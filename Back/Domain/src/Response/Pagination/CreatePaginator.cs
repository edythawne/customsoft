using Domain.Request;

namespace Domain.Response.Pagination;

public class CreatePaginator {
    
    private PaginationResult<T> Paginate<T>(HttpContextInformation contextInformation, BasePaginatedRequest request, List<T>? items, long totalItems) {
        var pagination = new Paginator {
            CurrentPage = request.Page,
            TotalItems = totalItems,
            ItemsPerPage = request.Limit == 0 ? totalItems : request.Limit,
            Path = contextInformation.Path,
            Query = contextInformation.Query,
            DataSize = items?.Count ?? 0
        };

        pagination.FirstPageUrl = pagination.GetUrl(1);
        pagination.CurrentPageUrl = pagination.GetUrl(pagination.CurrentPage);
        pagination.LastPageUrl = pagination.GetUrl(pagination.LastPage);
        
        return new PaginationResult<T> { Data = items, Pagination = pagination };
    }

    public static PaginationResult<T> GetPaginator<T>(HttpContextInformation contextInformation, BasePaginatedRequest request, List<T>? data, long totalItems) {
        var paginator = new CreatePaginator();
        return paginator.Paginate(contextInformation, request, data, totalItems);
    }

    
}