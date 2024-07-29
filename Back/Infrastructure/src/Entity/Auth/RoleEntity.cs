using Newtonsoft.Json;

namespace Infrastructure.Entity.Auth;

public class RoleEntity {
    
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
}