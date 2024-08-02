using Domain.Command.App;
using Domain.Response;
using Infrastructure.Repository;
using MediatR;

namespace Domain.Handler.App;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, BaseResponse> {
    
    private readonly UserRepository Repository;

    public GetUserByIdQueryHandler(UserRepository repository) {
        Repository = repository;
    }

    public async Task<BaseResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
        var dictionary = new Dictionary<string, object> {
            { "_id", request.Request.Id }
        };
        
        Repository.SetParameters(dictionary);
        var response = await Repository.GetUserById();
        
        return new BaseResponse(response!.Message!, response.Data);
    }
    
}