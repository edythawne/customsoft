namespace Infrastructure.Exception;

public abstract class BaseException : System.Exception {
    public int Code { private set; get; }

    protected BaseException(int code, string exception) : base(exception) {
        Code = code;
    }
    
}