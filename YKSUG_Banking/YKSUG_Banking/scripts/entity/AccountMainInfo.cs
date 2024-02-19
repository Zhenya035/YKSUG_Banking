using System.Collections.Generic;
using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity
{
    public class AccountMainInfo
    {
        [JsonProperty(PropertyName="username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName="card")]
        public CardMainInfo Card { get; set; }
        [JsonProperty(PropertyName="bonuses")]
        public List<BoughtBonusInfo> Bonus { get; set; }
    }
}