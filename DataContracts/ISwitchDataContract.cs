using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ISwitchDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string Dependancy { get; set; }
        Status Status { get; set; }
        Status OnDependancyStatus { get; set; }
        Status ForceStatus { get; set; }
        bool IsForced { get; set; }
    }
}
