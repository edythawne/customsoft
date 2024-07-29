using AutoMapper;
using Domain.Command.App;
using Domain.Response;
using Domain.Response.Pagination;
using Infrastructure.Dto.App;
using Infrastructure.Entity.App;
using Infrastructure.Repository;
using Infrastructure.Repository.Common;
using MediatR;

namespace Domain.Handler.App;

public class GetUserPaginatedCommandHandler : IRequestHandler<GetUserPaginatedCommand, BaseResponse>, IGetPaginated<UserPaginatedEntity> {
    
    private readonly AppRepository Repository;
    private readonly IMapper Mapper;

    public GetUserPaginatedCommandHandler(AppRepository repository, IMapper mapper) {
        Repository = repository;
        Mapper = mapper;
    }

    public async Task<BaseResponse> Handle(GetUserPaginatedCommand request, CancellationToken cancellationToken) {
        var dto = Mapper.Map<UserPaginatedDto>(request.Request);
        var dictionary = new Dictionary<string, object> {
            { "_active", true },
            { "_page", dto.Page },
            { "_limit", dto.Limit },
        };

        if (dto.Search != null) {
            dictionary.Add("_search", dto.Search);
        }
        
        Repository.SetParameters(dictionary);
        return await ParserPaginated.SetResponsePagination(this, request.Request, request.http);
    }
    
    public async Task<BaseResponseEntity<UserPaginatedEntity>?> SetPaginated() {
        return await Repository.GetAllPaginated();
    }
    
}