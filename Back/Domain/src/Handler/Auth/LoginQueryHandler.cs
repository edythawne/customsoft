using AutoMapper;
using Domain.Command.Auth;
using Domain.Response;
using Infrastructure.Dto.Auth;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Handler.Auth;

public class LoginQueryHandler: IRequestHandler<LoginQuery, BaseResponse> {
    
    private readonly ILogger<LoginQueryHandler> Logger;
    private readonly AuthRepository Repository;
    private readonly TokenService Tokenable;
    private readonly IMapper Mapper;
    
    public LoginQueryHandler(AuthRepository repository, IMapper mapper, TokenService tokenService, ILogger<LoginQueryHandler> logger) {
        Repository = repository;
        Tokenable = tokenService;
        Mapper = mapper;
        Logger = logger;
    }

    public async Task<BaseResponse> Handle(LoginQuery query, CancellationToken cancellationToken) {
        Logger.LogInformation("Se ha recibido una solicitud de inicio de session");
        
        var dto = Mapper.Map<LoginDto>(query.LoginRequest);
        Repository.SetParameters(new Dictionary<string, object> {{ "_data", JsonConvert.SerializeObject(dto)}});
        
        var response = await Repository.Login();
        
        if (response != null && response.Data != null) {
            Logger.LogInformation($"Ha iniciado session el usuario {response.Data.Email}");
            
            var token = Tokenable.GenerateToken(response!.Data!.Id);
            response!.Data!.Token = token;
            return new BaseResponse(response!.Message!, response.Data);
        }
        
        Logger.LogInformation($"El usuario con {dto.Email} ha fallado al iniciar sesión");
        return new BaseResponse("Usuario no existe", null);
    }

}