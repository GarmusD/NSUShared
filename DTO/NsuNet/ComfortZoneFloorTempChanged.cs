using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DTO.NsuNet
{
    public struct ComfortZoneFloorTempChanged
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
        public double Value { get; set; }


        public static ComfortZoneFloorTempChanged Create(string czName, double value)
        {
            return new ComfortZoneFloorTempChanged()
            {
                Target = JKeys.ComfortZone.TargetName,
                Action = JKeys.Action.Info,
                Name = czName,
                Value = value,
                Content = JKeys.ComfortZone.CurrentFloorTemp
            };
        }
    }
}
