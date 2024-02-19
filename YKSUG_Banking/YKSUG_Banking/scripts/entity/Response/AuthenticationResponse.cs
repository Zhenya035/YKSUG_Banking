using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Response
{
    public class AuthenticationResponse
    {
        [JsonProperty(PropertyName="token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName="role")]
        public string Role { get; set; }
    }
}