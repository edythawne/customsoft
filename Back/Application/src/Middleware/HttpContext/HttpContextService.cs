using Domain;

namespace Application.Middleware.HttpContext;

public class HttpContextService : ICustomHttpContextService  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor) {
        Console.WriteLine("HttpContextService");
        _httpContextAccessor = httpContextAccessor;
    }

    public HttpContextInformation CreateHttpContext() {
        var request = _httpContextAccessor.HttpContext?.Request;
        
        var path = request!.Path.HasValue ? request.Path.Value : null;
        var query = request.QueryString.HasValue ? request.QueryString.Value : null;
        var token = request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        return new HttpContextInformation(path, query, token);
    }
}