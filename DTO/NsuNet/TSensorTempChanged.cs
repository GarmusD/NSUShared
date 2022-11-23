using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DTO.NsuNet
{
    public struct TSensorTempChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.TempSensor.SensorID)]
        public string SensorID { get; set; }
        [JsonProperty(JKeys.TempSensor.Temperature)]
        public double Temperature { get; set; }
        [JsonProperty(JKeys.TempSensor.ReadErrors)]
        public int ReadErrorCount { get; set; }

        public static TSensorTempChanged Create(string sensorId, double temperature, int readErrorCount)
        {
            return new TSensorTempChanged 
            { 
                Target = JKeys.TempSensor.TargetName,
                Action = JKeys.Action.Info,
                SensorID = sensorId, 
                Temperature = temperature, 
                ReadErrorCount = readErrorCount 
            };
        }
    }
}
