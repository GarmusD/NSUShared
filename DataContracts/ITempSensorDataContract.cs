using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ITempSensorDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        byte[] SensorID { get; set; }
        int Interval { get; set; }
        double Temperature { get; set; }
        bool NotFound { get; set; }
        int ReadErrorCount { get; set; }
    }
}
