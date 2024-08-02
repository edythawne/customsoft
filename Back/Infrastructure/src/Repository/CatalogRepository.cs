using Infrastructure.Entity.Catalog;
using Infrastructure.Exception;
using Infrastructure.Repository.Common;

namespace Infrastructure.Repository;

public class CatalogRepository: BaseRepository {
    
    public async Task<BaseResponseEntity<DepartmentEntity[]>?> GetAllDepartment() {
        await OpenConnection();

        try {
            return await ApplyProcedure<BaseResponseEntity<DepartmentEntity[]>>("catalog.department_get_all");
        } catch (System.Exception exception) {
            // Tratamiento de exception
            throw new EntityException(exception.Message);
        } finally {
            CloseConnection();
        }
    }
    
}