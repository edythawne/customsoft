using Infrastructure.Entity.App;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class AppRepository : BaseRepository {
    
    public async Task<BaseResponseEntity<UserPaginatedEntity>?> GetAllPaginated() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<UserPaginatedEntity>>("app.user_get_all_paginated");
        } catch (Exception exception) {
            throw new Exception(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
    public async Task<BaseResponseEntity<int>?> SyncUserDetail() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<int>>("app.user_sync_detail");
        } catch (Exception exception) {
            throw new Exception(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
}