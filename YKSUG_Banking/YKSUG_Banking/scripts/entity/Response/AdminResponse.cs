using Newtonsoft.Json;

namespace YKSUG_Banking.scripts.entity.Response
{
    public class AdminResponse
    {
        [JsonProperty(PropertyName = "state")] public string State { get; set; }
    }
}