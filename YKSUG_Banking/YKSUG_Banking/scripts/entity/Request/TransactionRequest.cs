using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Request
{
    public class TransactionRequest
    {
        [JsonProperty(PropertyName = "fromCard")]
        public string fromCard { get; set; }

        [JsonProperty(PropertyName = "toCard")]
        public string toCard { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public long amount { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
    }
}