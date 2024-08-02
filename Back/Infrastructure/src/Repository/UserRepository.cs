using Infrastructure.Entity.Auth;
using Infrastructure.Exception;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class UserRepository : BaseRepository {

    public async Task<BaseResponseEntity<int>?> CreateUserWithRolAndDepartment() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<int>>("app.user_store");
        } catch (System.Exception exception) {
            throw new EntityException(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
    public async Task<BaseResponseEntity<UserEntity>?> GetUserById() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<UserEntity>>("app.user_get_by_id");
        } catch (System.Exception exception) {
            throw new EntityException(exception.Message);
        } finally {
            CloseConnection();
        }
    }

}