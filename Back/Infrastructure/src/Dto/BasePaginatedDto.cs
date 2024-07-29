using Newtonsoft.Json;

namespace Infrastructure.Dto;

public abstract class BasePaginatedDto {
    
    [JsonProperty("limit")]
    public int Limit { get; set; }
    
    [JsonProperty("page")]
    public int Page { get; set; }
    
    
}