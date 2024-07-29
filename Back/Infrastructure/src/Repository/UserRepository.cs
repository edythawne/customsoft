using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class UserRepository : BaseRepository {

    public async Task<BaseResponseEntity<int>?> CreateUserWithRolAndDepartment() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<int>>("app.user_store");
        } catch (Exception exception) {
            // Tratamiento de exception
            throw new Exception(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    

}