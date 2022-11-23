using Newtonsoft.Json;
using NSU.Shared;

namespace NSU.Shared.DTO.NsuNet
{
    public struct HandshakeResponse
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.ServerInfo.Version)]
        public string Version { get; set; }
        [JsonProperty(JKeys.ServerInfo.Protocol)]
        public int Protocol { get; set; }
    }
}
