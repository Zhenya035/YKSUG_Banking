using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Request
{
    public class AdminTransactionRequest
    {
        [JsonProperty(PropertyName="cardNumber")]
        public string cardNumber { get; set; }
        [JsonProperty(PropertyName="amount")]
        public long amount { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
    }
}