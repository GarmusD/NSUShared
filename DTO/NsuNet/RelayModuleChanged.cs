using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DTO.NsuNet
{
    public struct RelayModuleChanged
    {
        [JsonProperty(JKeys.Generic.Target)]
        public string Target { get; set; }
        [JsonProperty(JKeys.Generic.Action)]
        public string Action { get; set; }
        [JsonProperty(JKeys.Generic.ConfigPos)]
        public byte ConfigPos { get; set; }
        [JsonProperty(JKeys.RelayModule.StatusFlags)]
        public byte StatusFlag { get; set; }
        [JsonProperty(JKeys.RelayModule.LockFlags)]
        public byte LockFlags { get; set; }

        public static RelayModuleChanged Create(byte configPos, byte statusFlags, byte lockFlags)
        {
            return new RelayModuleChanged()
            {
                Target = JKeys.RelayModule.TargetName,
                Action = JKeys.Action.Info,
                ConfigPos = configPos,
                StatusFlag = statusFlags,
                LockFlags = lockFlags
            };
        }
    }

    public struct RelayModuleStatus
    {
        
    }
}
