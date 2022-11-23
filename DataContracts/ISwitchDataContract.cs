using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ISwitchDataContract : INSUSysPartDataContract
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ConfigPos { get; set; }
        public string Dependancy { get; set; }
        public Status Status { get; set; }
        public Status OnDependancyStatus { get; set; }
        public Status ForceStatus { get; set; }
        public bool IsForced { get; set; }
    }
}
