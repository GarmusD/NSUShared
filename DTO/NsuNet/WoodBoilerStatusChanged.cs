using Newtonsoft.Json;

namespace NSU.Shared.DTO.NsuNet
{
    public struct WoodBoilerStatusChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Content)]
        public string Content { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.Generic.Value)]
        public string Value { get; set; }


        public static WoodBoilerStatusChanged CreateForWB(string name, string status)
        {
            return new WoodBoilerStatusChanged
            {
                Target = JKeys.WoodBoiler.TargetName,
                Action = JKeys.Action.Info,
                Content = JKeys.Generic.Status,
                Name = name,
                Value = status
            };
        }

        public static WoodBoilerStatusChanged CreateForLadomat(string name, string status)
        {
            return new WoodBoilerStatusChanged
            {
                Target = JKeys.WoodBoiler.TargetName,
                Action = JKeys.Action.Info,
                Content = JKeys.WoodBoiler.LadomatStatus,
                Name = name,
                Value = status
            };
        }

        public static WoodBoilerStatusChanged CreateForExhaustFan(string name, string status)
        {
            return new WoodBoilerStatusChanged
            {
                Target = JKeys.WoodBoiler.TargetName,
                Action = JKeys.Action.Info,
                Content = JKeys.WoodBoiler.ExhaustFanStatus,
                Name = name,
                Value = status
            };
        }
    }
}
