using Infrastructure.Entity.App;
using Infrastructure.Repository.Common;

namespace Domain.Response.Pagination;

public interface IGetPaginated<T> {
    
    public Task<BaseResponseEntity<UserPaginatedEntity>?> SetPaginated();
    
}