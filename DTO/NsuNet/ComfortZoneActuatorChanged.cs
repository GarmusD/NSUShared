using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
    /// <summary>
    /// Data Transfer Object for ComfortZone ActuatorChanged value.
    /// </summary>
    public struct ComfortZoneActuatorOpenedChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.Generic.Content)]
        public string Content { get; set; }
        [JsonProperty(JKeys.Generic.Value)]
        public bool Value { get; set; }


        public static ComfortZoneActuatorOpenedChanged Create(string czName, bool value)
        {
            return new ComfortZoneActuatorOpenedChanged()
            {
                Target = JKeys.ComfortZone.TargetName,
                Action = JKeys.Action.Info,
                Name = czName,
                Value = value,
                Content = JKeys.ComfortZone.ActuatorOpened
            };
        }
    }
}
