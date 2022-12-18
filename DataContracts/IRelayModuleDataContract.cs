using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IRelayModuleDataContract : INSUSysPartDataContract
    {
        [JsonProperty(JKeys.Generic.ConfigPos)]
        byte ConfigPos { get; set; }
        [JsonProperty(JKeys.Generic.Enabled)]
        bool Enabled { get; set; }
        [JsonProperty(JKeys.RelayModule.ActiveLow)]
        bool ActiveLow { get; set; }
        [JsonProperty(JKeys.RelayModule.Reversed)]
        bool ReversedOrder { get; set; }
        [JsonProperty(JKeys.RelayModule.StatusFlags)]
        byte StatusFlags { get; set; }
        [JsonProperty(JKeys.RelayModule.LockFlags)]
        byte LockFlags { get; set; }
    }
}
