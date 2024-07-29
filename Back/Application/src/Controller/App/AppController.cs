using Domain.Command.App;
using Domain.Request.App;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controller.App;

[ApiController]
[Route("api/[controller]")]
public class AppController : BaseController {

    public AppController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator, httpContextAccessor) {
        
    }

    [HttpPost("store")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) {
        return await Execute(new CreateUserCommand { UserRequest = request});
    }
    
    [HttpPost("index")]
    public async Task<IActionResult> GetUserPaginated(UserPaginatedRequest request) {
        return await Execute(new GetUserPaginatedCommand { Request = request, http = GetHttpContext()});
    }
    
    [HttpPost("store/detail")]
    public async Task<IActionResult> SyncUserDetail([FromBody] CreateUserDetailRequest request) {
        return await Execute(new SyncUserDetailCommand { Request = request, Http = GetHttpContext()});
    }

}