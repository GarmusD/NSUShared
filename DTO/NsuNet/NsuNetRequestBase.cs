using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
    public class NsuNetRequestBase
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; } = string.Empty;
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; } = string.Empty;
        [JsonProperty(JKeys.Generic.CommandID)]
        public string CommandId { get; set; }
        [JsonProperty(JKeys.Generic.ResponseRequired)]
        public bool ResponseRequired { get; set; }
    }
}
