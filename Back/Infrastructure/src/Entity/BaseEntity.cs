using Newtonsoft.Json;

namespace Infrastructure.Entity;

public abstract class BaseEntity {
    
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
    
}