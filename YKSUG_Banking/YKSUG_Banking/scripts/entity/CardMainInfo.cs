using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity
{
    public class CardMainInfo
    {
        [JsonProperty(PropertyName="cardNumber")]
        public string CardNumber { get; set; }
        [JsonProperty(PropertyName="amount")]
        public long Amount { get; set; }
    }
}