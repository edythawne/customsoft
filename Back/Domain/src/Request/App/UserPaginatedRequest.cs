namespace Domain.Request.App;

public class UserPaginatedRequest : BasePaginatedRequest {
    
    public string? Search { set; get; } = null!;
    
}