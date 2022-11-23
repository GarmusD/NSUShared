using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ISystemFanDataContract : INSUSysPartDataContract
    {
        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string TempSensorName { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int CurrentPWM { get; set; }
    }
}
