using Domain.Request.App;
using Domain.Response;
using MediatR;

namespace Domain.Command.App;

public class GetUserPaginatedCommand : IRequest<BaseResponse> {

    public UserPaginatedRequest Request { get; set; } = null!;

    public HttpContextInformation? http { get; set; }

}