using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity
{
    public class BonusMainData
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "price")]
        public long price { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public long amount { get; set; }
    }
}