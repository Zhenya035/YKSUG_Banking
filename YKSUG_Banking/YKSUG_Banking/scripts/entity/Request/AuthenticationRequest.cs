using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Request
{
    public class AuthenticationRequest
    {
            [JsonProperty(PropertyName="username")]
            public string Username { get; set; }
            [JsonProperty(PropertyName="password")]
            public string Password { get; set; }
    }
}