namespace Domain.Response.Pagination;

public class PaginationResult<T> {
    
    public List<T>? Data { get; set; }
    public Paginator? Pagination { get; set; }
    
}