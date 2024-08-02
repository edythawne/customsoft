using Domain.Response;
using MediatR;

namespace Domain.Command.Auth;

public class GetAllRoleQuery : IRequest<BaseResponse> {

    public HttpContextInformation? http { get; set; }
    
}