using Domain.Command.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controller.Catalog;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RoleController : BaseController {
    
    public RoleController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator, httpContextAccessor) {
        
    }
    
    [HttpGet("get/all")]
    public async Task<IActionResult> GetAll() {
        return await Execute(new GetAllRoleQuery { http = GetHttpContext()});
    }
    
}