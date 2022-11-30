using Newtonsoft.Json;

namespace NHService;

public class UserModel
{
    [JsonProperty("userName")]
    public string? UserName { get; set; }
    
    [JsonProperty("userPhone")]
    public string? UserPhone { get; set; }

    [JsonProperty("userPassword")]
    public string? UserPassword { get; set; }
}