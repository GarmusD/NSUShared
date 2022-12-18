using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
#nullable enable
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
        [JsonProperty(JKeys.Generic.CommandID)]
        public string? CommandID { get; set; }
    }
#nullable disable
}
