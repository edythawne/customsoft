using Microsoft.AspNetCore.Http;

namespace Infrastructure.Exception;

public class EntityException : BaseException {

    public EntityException(string message) : base(StatusCodes.Status500InternalServerError, message) {
            
    }

}