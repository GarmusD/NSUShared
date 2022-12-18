using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IThermoActuatorDataContract
    {
        byte Index { get; }
        ActuatorType Type { get; set; }
        byte RelayChannel { get; set; }
        bool? Opened { get; set; }
    }
}
