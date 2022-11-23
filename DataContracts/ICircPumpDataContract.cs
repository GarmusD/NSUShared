using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ICircPumpDataContract : INSUSysPartDataContract
    {
        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string TempTriggerName { get; set; }
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public int Spd1Channel { get; set; }
        public int Spd2Channel { get; set; }
        public int Spd3Channel { get; set; }
        public Status Status { get; set; }
        public int OpenedValvesCount { get; set; }
    }
}
