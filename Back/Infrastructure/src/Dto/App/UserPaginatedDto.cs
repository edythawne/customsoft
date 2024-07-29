using Newtonsoft.Json;

namespace Infrastructure.Dto.App;

public class UserPaginatedDto : BasePaginatedDto {
    
    [JsonProperty("search")]
    public string? Search { set; get; }
    
}