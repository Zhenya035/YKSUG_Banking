using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity
{
    public class TransactionMainInfo
    {
        [JsonProperty(PropertyName="transactionID")]
        public long transactionID { get; set; }
        [JsonProperty(PropertyName="fromCard")]
        public string fromCard { get; set; }
        [JsonProperty(PropertyName="toCard")]
        public string toCard { get; set; }
        [JsonProperty(PropertyName="amount")]
        public long amount { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName="date")]
        public string date { get; set; }
    }
}