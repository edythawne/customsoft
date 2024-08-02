using Domain.Command.Catalog;
using Domain.Response;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Handler.Catalog;

public class DepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, BaseResponse> {
    
    private readonly ILogger<DepartmentQueryHandler> Logger;
    private readonly CatalogRepository Repository;
    private readonly TokenService Tokenable;
    
    public DepartmentQueryHandler(CatalogRepository repository, TokenService tokenService, ILogger<DepartmentQueryHandler> logger) {
        Repository = repository;
        Tokenable = tokenService;
        Logger = logger;
    }
    
    public async Task<BaseResponse> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken) {
        Logger.LogInformation("Se ha recibido una solicitud de consulta de departamentos");
        var response = await Repository.GetAllDepartment();
        
        if (response != null && response.Data != null) {
            return new BaseResponse(response!.Message!, response!.Data);
        }
        
        return new BaseResponse("Sin información", null);
    }
    
    
}