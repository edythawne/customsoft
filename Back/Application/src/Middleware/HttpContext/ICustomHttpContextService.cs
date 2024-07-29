using Domain;

namespace Application.Middleware.HttpContext;

public interface ICustomHttpContextService {
    
    HttpContextInformation CreateHttpContext();
    
}