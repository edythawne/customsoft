using Newtonsoft.Json;

namespace Infrastructure.Dto.App;

public class CreateUserDto {
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("first_name")]
    public string FirstName { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("password")]
    public string Password { get; set; }
    
    [JsonProperty("fk_department")]
    public int FkDepartment { get; set; }
    
    [JsonProperty("fk_rol")]
    public int FkRol { get; set; }
    
    [JsonProperty("created_by")]
    public int CreatedBy { get; set; }
    
}