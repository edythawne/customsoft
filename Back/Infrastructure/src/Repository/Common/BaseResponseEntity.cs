using Newtonsoft.Json;

namespace Infrastructure.Repository.Common;

public class BaseResponseEntity<T> {
    
    [JsonProperty("message")]
    public string? Message { set; get; }
    
    [JsonProperty("data")]
    public T? Data { set; get; }
    
}