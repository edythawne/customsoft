using Domain.Command.Auth;
using Domain.Request.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controller.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController: BaseController {
    
    public AuthController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : base(mediator, httpContextAccessor) {
        
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) {
        return await Execute(new LoginCommand() { LoginRequest = request});
    }
    
}