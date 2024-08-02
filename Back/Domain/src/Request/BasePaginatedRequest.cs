namespace Domain.Request;

public abstract class BasePaginatedRequest {

    public int Limit { get; private set; } = 15;
    public int Page { get; set; } = 1;

    
}