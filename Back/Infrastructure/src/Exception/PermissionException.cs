namespace Infrastructure.Exception;

public class PermissionException : AuthenticationException {

    public PermissionException(string message) : base(message) {
            
    }

}