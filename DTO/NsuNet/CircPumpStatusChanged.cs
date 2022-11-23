using Newtonsoft.Json;
using NSUWatcher.Interfaces.MCUCommands;

namespace NSU.Shared.DTO.NsuNet
{
    public struct CircPumpStatusChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.Generic.Status)]
        public string Status { get; set; }
        [JsonProperty(JKeys.CircPump.CurrentSpeed)]
        public string CurrentSpeed { get; set; }
        [JsonProperty(JKeys.CircPump.ValvesOpened)]
        public string ActuatorsOpened { get; set; }

        public static CircPumpStatusChanged Create(string name, string status, string currentSpeed, string actuatorsOpened)
        {
            return new CircPumpStatusChanged()
            {
                Target = JKeys.CircPump.TargetName,
                Action = JKeys.Action.Info,
                Name = name,
                Status = status,
                CurrentSpeed = currentSpeed,
                ActuatorsOpened = actuatorsOpened
            };
        }
    }
}
