using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IThermoActuatorDataContract
    {
        public int Index { get; }
        public ActuatorType Type { get; set; }
        public int RelayChannel { get; set; }
        public bool? Opened { get; set; }
    }
}
