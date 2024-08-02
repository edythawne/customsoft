using Microsoft.AspNetCore.Http;

namespace Infrastructure.Exception;

public class NullResponseException : BaseException {

    public NullResponseException(string exception) : base(StatusCodes.Status202Accepted, exception) {
        
    }
    
}