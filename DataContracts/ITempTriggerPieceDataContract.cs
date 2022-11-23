using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface ITempTriggerPieceDataContract
    {
        public int Index { get; set; }
        public bool Enabled { get; set; }
        public string TSensorName { get; set; }
        public TriggerCondition Condition { get; set; }
        public double Temperature { get; set; }
        public double Histeresis { get; set; }
    }
}
