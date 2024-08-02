using AutoMapper;
using Domain.Command.App;
using Domain.Response;
using Infrastructure.Dto.App;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Handler.App;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponse> {

    private readonly ILogger<CreateUserCommandHandler> Logger;
    private readonly UserRepository Repository;
    private readonly IMapper Maper;

    public CreateUserCommandHandler(UserRepository repository, IMapper mapper, ILogger<CreateUserCommandHandler> logger) {
        Logger = logger;
        Repository = repository;
        Maper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
        Logger.LogInformation("Se ha realizado una petición para crear un usuario");
        
        var dto = Maper.Map<CreateUserDto>(request.UserRequest);
        dto.Password = "Hello World";
        dto.CreatedBy = 1;
        
        Repository.SetParameters(new Dictionary<string, object> {{ "_data", JsonConvert.SerializeObject(dto)}});
        var response = await Repository.CreateUserWithRolAndDepartment();
        return new BaseResponse(response!.Message!, response.Data);
    }
    
}