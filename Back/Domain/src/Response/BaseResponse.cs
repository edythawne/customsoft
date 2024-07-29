namespace Domain.Response;

public class BaseResponse {

    public BaseResponse() {
        
    }

    public BaseResponse(string message, object? data) {
        Data = data;
        Message = message;
    }

    public object? Data { get; set; }

    public string? Message { get; set; }

}