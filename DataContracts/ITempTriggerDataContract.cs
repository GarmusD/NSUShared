using NSU.Shared.NSUSystemPart;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface ITempTriggerDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        Status Status { get; set; }
        ITempTriggerPieceDataContract[] TempTriggerPieces { get; }
    }
}
