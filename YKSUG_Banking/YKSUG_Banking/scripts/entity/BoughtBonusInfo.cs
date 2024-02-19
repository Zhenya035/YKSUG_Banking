using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity
{
    public class BoughtBonusInfo
    {
        [JsonProperty(PropertyName="name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName="description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName="token")]
        public string Token { get; set; }
    }
}