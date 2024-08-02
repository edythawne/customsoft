
using Infrastructure.Dto;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Exception;

public abstract class TokenException {
    
    public class Create : BaseException {
        
        public Create(string message) : base(StatusCodes.Status500InternalServerError, $"{MessageConstant.TokenCreateException} : message") {
            
        }

        
    }
    
    public class Read : BaseException {
        
        public Read(string message) : base(StatusCodes.Status500InternalServerError, $"{MessageConstant.TokenReadException} : message") {
            
        }

        
    }
    
}