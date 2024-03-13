using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Request
{
    public class AdminCheckBonusTokenRequest
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "token")] public string Token { get; set; }
    }
}