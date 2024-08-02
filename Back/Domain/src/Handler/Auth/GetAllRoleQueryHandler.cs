using Domain.Command.Auth;
using Domain.Response;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Handler.Auth;

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, BaseResponse>{
    
    private readonly ILogger<GetAllRoleQueryHandler> Logger;
    private readonly AuthRepository Repository;
    private readonly TokenService Tokenable;
    
    public GetAllRoleQueryHandler(AuthRepository repository, TokenService tokenService, ILogger<GetAllRoleQueryHandler> logger) {
        Repository = repository;
        Tokenable = tokenService;
        Logger = logger;
    }
    
    public async Task<BaseResponse> Handle(GetAllRoleQuery request, CancellationToken cancellationToken) {
        Logger.LogInformation("Se ha recibido una solicitud de consulta de roles");
        var response = await Repository.GetAllRoles();

        if (response != null && response.Data != null) {
            return new BaseResponse(response!.Message!, response!.Data);
        }
        
        return new BaseResponse("Sin información", null);
    }
}