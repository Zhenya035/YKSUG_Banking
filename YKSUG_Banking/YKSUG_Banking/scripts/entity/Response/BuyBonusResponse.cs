using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Response
{
    public class BuyBonusResponse
    {
        [JsonProperty(PropertyName="token")]
        public string Token { get; set; }
    }
}