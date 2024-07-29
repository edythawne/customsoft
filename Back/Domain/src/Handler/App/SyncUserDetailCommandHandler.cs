using AutoMapper;
using Domain.Command.App;
using Domain.Response;
using Infrastructure.Dto.App;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Handler.App;

public class SyncUserDetailCommandHandler : IRequestHandler<SyncUserDetailCommand, BaseResponse> {
    
    private readonly ILogger<SyncUserDetailCommandHandler> Logger;
    private readonly AppRepository Repository;
    private readonly IMapper Maper;

    public SyncUserDetailCommandHandler(AppRepository repository, IMapper mapper, ILogger<SyncUserDetailCommandHandler> logger) {
        Repository = repository;
        Maper = mapper;
        Logger = logger;
    }

    public async Task<BaseResponse> Handle(SyncUserDetailCommand request, CancellationToken cancellationToken) {
        Logger.LogInformation("Se ha recibido una solicitud para actualizar datos");
        var dto = Maper.Map<CreateUserDetailDto>(request.Request);
        dto.CreatedBy = 1;
        
        Repository.SetParameters(new Dictionary<string, object> {{ "_data", JsonConvert.SerializeObject(dto)}});
        var response = await Repository.SyncUserDetail();
        return new BaseResponse(response!.Message!, response.Data);
    }

    
}