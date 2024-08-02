using Domain.Command.App;
using Domain.Request.App;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controller.App;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class AppController : BaseController {

    public AppController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator, httpContextAccessor) {
        
    }

    [HttpPost("user/store")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) {
        return await Execute(new CreateUserCommand { UserRequest = request});
    }
    
    [HttpGet("user/index")]
    public async Task<IActionResult> GetUserPaginated([FromQuery] UserPaginatedRequest request) {
        return await Execute(new GetUserPaginatedQuery { Request = request, http = GetHttpContext()});
    }
    
    [HttpPost("store/detail")]
    public async Task<IActionResult> SyncUserDetail([FromBody] CreateUserDetailRequest request) {
        return await Execute(new SyncUserDetailCommand { Request = request, Http = GetHttpContext()});
    }

    [HttpGet("user/by/id")]
    public async Task<IActionResult> GetUserById([FromQuery] GetUserByIdRequest request) {
        return await Execute(new GetUserByIdQuery { Request = request, http = GetHttpContext()});
    }
}
