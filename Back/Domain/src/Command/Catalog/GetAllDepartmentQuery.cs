using Domain.Response;
using MediatR;

namespace Domain.Command.Catalog;

public class GetAllDepartmentQuery : IRequest<BaseResponse> {

    public HttpContextInformation? http { get; set; }
    
}