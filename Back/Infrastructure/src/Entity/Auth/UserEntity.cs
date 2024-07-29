using Infrastructure.Entity.Catalog;
using Newtonsoft.Json;

namespace Infrastructure.Entity.Auth;

public class UserEntity : BaseEntity {
    
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("token")]
    public object Token { get; set; }

    [JsonProperty("roles")]
    public List<RoleEntity> Roles { get; set; }

    [JsonProperty("department")]
    public DepartmentEntity Department { get; set; }
    
}