using Domain.Request.App;
using Domain.Response;
using MediatR;

namespace Domain.Command.App;

public class GetUserByIdQuery : IRequest<BaseResponse> {

    public GetUserByIdRequest Request { get; set; } = null!;

    public HttpContextInformation? http { get; set; }

}