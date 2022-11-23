using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface IWoodBoilerDataContract : INSUSysPartDataContract
    {
        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string TSensorName { get; set; }
        public string KTypeName { get; set; }
        public int LadomChannel { get; set; }
        public Status LadomStatus { get; set; }
        public bool LadomatIsManual { get; set; }
        public int ExhaustFanChannel { get; set; }
        public Status ExhaustFanStatus { get; set; }
        public bool ExhaustFanIsManual { get; set; }
        public double WorkingTemp { get; set; }
        public double Histeresis { get; set; }
        public WoodBoilerStatus WBStatus { get; set; }
        public double CurrentTemp { get; set; }
        public WoodBoilerTempStatus TempStatus { get; set; }
        public double LadomatTemp { get; set; }
        public string LadomatTriggerName { get; set; }
        public string WaterBoilerName { get; set; }
    }
}
