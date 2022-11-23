using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DTO.NsuNet
{
    public struct TempTriggerStatusChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.Name)]
        public string Name { get; set; }
        [JsonProperty(JKeys.Generic.Status)]
        public string Status { get; set; }
        
        public static TempTriggerStatusChanged Create(string name, string status)
        {
            return new TempTriggerStatusChanged 
            {
                Target = JKeys.TempTrigger.TargetName,
                Action = JKeys.Action.Info,
                Name = name,
                Status = status
            };
        }
    }
}
