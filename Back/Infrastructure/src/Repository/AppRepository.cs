using Infrastructure.Entity.App;
using Infrastructure.Exception;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class AppRepository : BaseRepository {
    
    public async Task<BaseResponseEntity<UserPaginatedEntity>?> GetAllPaginated() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<UserPaginatedEntity>>("app.user_get_all_paginated");
        } catch (System.Exception exception) {
            throw new EntityException(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
    public async Task<BaseResponseEntity<int>?> SyncUserDetail() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<int>>("app.user_sync_detail");
        } catch (System.Exception exception) {
            throw new EntityException(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
}