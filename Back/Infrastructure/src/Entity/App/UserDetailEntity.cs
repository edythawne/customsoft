using Newtonsoft.Json;

namespace Infrastructure.Entity.App;

public class UserDetailEntity {
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("curp")]
    public long Curp { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonProperty("place_of_birth")]
    public string PlaceOfBirth { get; set; }

    [JsonProperty("birth_certificate")]
    public string BirthCertificate { get; set; }
}