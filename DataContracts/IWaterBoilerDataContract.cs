using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface IWaterBoilerDataContract : INSUSysPartDataContract
    {
        public const int MAX_WATERBOILER_EL_HEATING_COUNT = 7;

        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string TempSensorName { get; set; }
        public string TempTriggerName { get; set; }
        public string CircPumpName { get; set; }
        public bool ElHeatingEnabled { get; set; }
        public int ElHeatingChannel { get; set; }
        public IElHeatingDataDataContract[] ElHeatingData { get; set; }
    }
}
