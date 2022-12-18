using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface IWaterBoilerDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string TempSensorName { get; set; }
        string TempTriggerName { get; set; }
        string CircPumpName { get; set; }
        bool ElHeatingEnabled { get; set; }
        int ElHeatingChannel { get; set; }
        IElHeatingDataDataContract[] ElHeatingData { get; set; }
    }
}
