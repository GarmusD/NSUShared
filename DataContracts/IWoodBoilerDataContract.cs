using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface IWoodBoilerDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string TSensorName { get; set; }
        string KTypeName { get; set; }
        int LadomChannel { get; set; }
        Status LadomStatus { get; set; }
        bool LadomatIsManual { get; set; }
        int ExhaustFanChannel { get; set; }
        Status ExhaustFanStatus { get; set; }
        bool ExhaustFanIsManual { get; set; }
        double WorkingTemp { get; set; }
        double Histeresis { get; set; }
        WoodBoilerStatus WBStatus { get; set; }
        double CurrentTemp { get; set; }
        WoodBoilerTempStatus TempStatus { get; set; }
        double LadomatTemp { get; set; }
        string LadomatTriggerName { get; set; }
        string WaterBoilerName { get; set; }
    }
}
