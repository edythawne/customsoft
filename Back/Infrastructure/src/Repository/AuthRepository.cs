using Infrastructure.Entity.Auth;
using Infrastructure.Exception;
using Infrastructure.Repository.Common;
using AuthenticationException = System.Security.Authentication.AuthenticationException;

namespace Infrastructure.Repository;

public class AuthRepository : BaseRepository {
    
    public async Task<BaseResponseEntity<UserEntity>?> Login() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<UserEntity>>("auth.user_by_login");
        } catch (System.Exception exception) {
            throw new AuthenticationException(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
    public async Task<BaseResponseEntity<RoleEntity[]>?> GetAllRoles() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<RoleEntity[]>>("auth.roles_get_all");
        } catch (System.Exception exception) {
            throw new EntityException(exception.Message);
        } finally {
            CloseConnection();
        }
    }

    
}