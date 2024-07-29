using Newtonsoft.Json;

namespace Infrastructure.Dto.Auth;

public class LoginDto {
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("password")]
    public string Password { get; set; }
    
}