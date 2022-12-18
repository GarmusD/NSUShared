using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ISystemFanDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string TempSensorName { get; set; }
        double MinTemp { get; set; }
        double MaxTemp { get; set; }
        int CurrentPWM { get; set; }
    }
}
