using Domain.Request.Auth;
using Domain.Response;
using MediatR;

namespace Domain.Command.Auth;

public class LoginQuery : IRequest<BaseResponse> {

    public LoginRequest LoginRequest { get; set; } = null!;
    
}