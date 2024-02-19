using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Request
{
    public class BuyBonusRequest
    {
        [JsonProperty(PropertyName="username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName="bonusName")]
        public string BonusName { get; set; }
    }
}