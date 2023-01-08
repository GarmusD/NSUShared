using Newtonsoft.Json;
using NSU.Shared;

namespace NSUShared.DTO.NsuNet
{
    public struct SystemPingRequest
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }

        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
    }
}
