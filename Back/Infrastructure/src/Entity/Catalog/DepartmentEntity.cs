using Newtonsoft.Json;

namespace Infrastructure.Entity.Catalog;

public class DepartmentEntity {
    
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("area_id")]
    public long AreaId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("area")]
    public AreaEntity? Area { get; set; }
    
}