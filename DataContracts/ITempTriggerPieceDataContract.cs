using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface ITempTriggerPieceDataContract
    {
        byte Index { get; set; }
        bool Enabled { get; set; }
        string TSensorName { get; set; }
        TriggerCondition Condition { get; set; }
        double Temperature { get; set; }
        double Histeresis { get; set; }
    }
}
