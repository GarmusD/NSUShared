using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IRelayModuleDataContract : INSUSysPartDataContract
    {
        public const int CHANNELS_PER_MODULE = 8;
        public const int MAX_RELAY_MODULES = 5;

        [JsonProperty(JKeys.Generic.ConfigPos)]
        public byte ConfigPos { get; set; }
        [JsonProperty(JKeys.Generic.Enabled)]
        public bool Enabled { get; set; }
        [JsonProperty(JKeys.RelayModule.ActiveLow)]
        public bool ActiveLow { get; set; }
        [JsonProperty(JKeys.RelayModule.Reversed)]
        public bool ReversedOrder { get; set; }
        [JsonProperty(JKeys.RelayModule.StatusFlags)]
        public byte StatusFlags { get; set; }
        [JsonProperty(JKeys.RelayModule.LockFlags)]
        public byte LockFlags { get; set; }
    }
}
