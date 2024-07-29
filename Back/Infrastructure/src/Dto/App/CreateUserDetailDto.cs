using Newtonsoft.Json;

namespace Infrastructure.Dto.App;

public class CreateUserDetailDto {
    
    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("curp")]
    public string Curp { get; set; }

    [JsonProperty("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonProperty("place_of_birth")]
    public string PlaceOfBirth { get; set; }

    [JsonProperty("birth_certificate")]
    public string BirthCertificate { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("created_by")]
    public long CreatedBy { get; set; }
    
}