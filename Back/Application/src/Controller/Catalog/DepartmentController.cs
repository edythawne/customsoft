using Domain.Command.Catalog;
using Domain.Handler.Catalog;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controller.Catalog;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DepartmentController: BaseController {
    
    public DepartmentController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator, httpContextAccessor) {
        
    }
    
    
    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll() {
        return await Execute(new GetAllDepartmentQuery { http = GetHttpContext()});
    }
    
    
}