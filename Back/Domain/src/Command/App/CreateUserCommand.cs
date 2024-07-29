using Domain.Request.App;
using Domain.Response;
using MediatR;

namespace Domain.Command.App;

public class CreateUserCommand : IRequest<BaseResponse> {

    public CreateUserRequest UserRequest { get; set; } = null!;

}