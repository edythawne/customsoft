using Domain.Request.App;
using Domain.Response;
using MediatR;

namespace Domain.Command.App;

public class SyncUserDetailCommand : IRequest<BaseResponse> {

    public CreateUserDetailRequest Request { get; set; }

    public HttpContextInformation? Http { get; set; }
    
}