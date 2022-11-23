using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ITempSensorDataContract : INSUSysPartDataContract
    {
        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public byte[] SensorID { get; set; }
        public int Interval { get; set; }
        public double Temperature { get; set; }
        public bool NotFound { get; set; }
        public int ReadErrorCount { get; set; }
    }
}
