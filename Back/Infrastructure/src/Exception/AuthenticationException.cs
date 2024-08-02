
using Infrastructure.Dto;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Exception;
    
public class AuthenticationException : BaseException {

    public AuthenticationException(string message = MessageConstant.ResponseBadAuthentication) : base(StatusCodes.Status401Unauthorized, message) {
            
    }

}