using Infrastructure.Entity.Auth;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class AuthRepository : BaseRepository {
    
    public async Task<BaseResponseEntity<UserEntity>?> Login() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<UserEntity>>("auth.user_by_login");
        } catch (Exception exception) {
            // Tratamiento de exception
            throw new Exception(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
}