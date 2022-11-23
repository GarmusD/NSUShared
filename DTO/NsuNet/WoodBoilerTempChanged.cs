using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
    public struct WoodBoilerTempChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Content)]
        public string Content { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.WoodBoiler.TemperatureStatus)]
        public string TemperatureStatus { get; set; }
        [JsonProperty(JKeys.WoodBoiler.CurrentTemp)]
        public double CurrentTemp { get; set; }

        public static WoodBoilerTempChanged Create(string name, string tempStatus, double temperature)
        {
            return new WoodBoilerTempChanged 
            {
                Target = JKeys.WoodBoiler.TargetName,
                Action = JKeys.Action.Info,
                Content = JKeys.WoodBoiler.TemperatureChange,
                Name = name,
                TemperatureStatus = tempStatus,
                CurrentTemp = temperature
            };
        }
    }
}
