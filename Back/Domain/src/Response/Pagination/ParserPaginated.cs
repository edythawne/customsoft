using Domain.Request;

namespace Domain.Response.Pagination;

public class ParserPaginated {

    /**
     * Permite realizar una respuesta de tipo paginador
     * <param name="callback">Clase que implementa paginador</param>
     * <param name="request">Clase que contiene limite y pagina del paginador</param>
     * <param name="httpContextInformation">Clase de contexto de http</param>
     */
    public static async Task<BaseResponse> SetResponsePagination<T>(IGetPaginated<T> callback, BasePaginatedRequest request, HttpContextInformation httpContextInformation) {
        var data = await callback.SetPaginated();
        
        if (data == null && data!.Data == null) {
            throw new Exception("NullResponseException");
        }
        
        var paginator = CreatePaginator.GetPaginator(httpContextInformation, request, data!.Data!.Data!, data!.Data.ItemsPaginated);
        return new BaseResponse("Ok", paginator);
    }
    
    
}