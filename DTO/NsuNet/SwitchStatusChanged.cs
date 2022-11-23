using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
    public struct SwitchStatusChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.Generic.Status)]
        public string Status { get; set; }
        [JsonProperty(JKeys.Switch.IsForced)]
        public bool IsForced { get; set; }

        public static SwitchStatusChanged Create(string name, string status, bool isForced)
        {
            return new SwitchStatusChanged()
            {
                Target = JKeys.Switch.TargetName,
                Action = JKeys.Action.Info,
                Name = name,
                Status = status,
                IsForced = isForced
            };
        }
    }
}
