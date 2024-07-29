using AutoMapper;
using Domain.Command.Auth;
using Domain.Response;
using Infrastructure.Dto.Auth;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Handler.Auth;

public class LoginCommandHandler: IRequestHandler<LoginCommand, BaseResponse> {
    
    private readonly ILogger<LoginCommandHandler> Logger;
    private readonly AuthRepository Repository;
    private readonly TokenService Tokenable;
    private readonly IMapper Mapper;
    
    public LoginCommandHandler(AuthRepository repository, IMapper mapper, TokenService tokenService, ILogger<LoginCommandHandler> logger) {
        Repository = repository;
        Tokenable = tokenService;
        Mapper = mapper;
        Logger = logger;
    }

    public async Task<BaseResponse> Handle(LoginCommand query, CancellationToken cancellationToken) {
        Logger.LogInformation("Se ha recibido una solicitud de inicio de session");
        var dto = Mapper.Map<LoginDto>(query.LoginRequest);
        Repository.SetParameters(new Dictionary<string, object> {{ "_data", JsonConvert.SerializeObject(dto)}});
        
        var response = await Repository.Login();
        
        if (response != null && response.Data != null) {
            var token = Tokenable.GenerateToken(response!.Data!.Id);
            response!.Data!.Token = token;
            Logger.LogInformation("Ha iniciado session un usuario");
            return new BaseResponse(response!.Message!, response.Data);
        }
        
        return new BaseResponse("Usuario no existe", null);
    }

}