using Newtonsoft.Json;

namespace Infrastructure.Entity.Catalog;

public class AreaEntity {
    
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
}