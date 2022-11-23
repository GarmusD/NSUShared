using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ICollectorDataContract : INSUSysPartDataContract
    {
        public const int MAX_COLLECTOR_ACTUATORS = 16;
        int ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string CircPumpName { get; set; }
        int ActuatorsCount { get; }
        IThermoActuatorDataContract[] Actuators { get; }
    }
}
