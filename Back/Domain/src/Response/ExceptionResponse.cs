namespace Domain.Response;

public class ExceptionResponse : BaseResponse {
    
    public int Code { set; get; }
    
    public ExceptionResponse(int code, string message) {
        Code = code;
        Data = null;
        Message = message;
    }
    
}