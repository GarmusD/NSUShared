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
        public const int MAX_TEMPTRIGGERPIECES = 4;

        public int ConfigPos { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public ITempTriggerPieceDataContract[] TempTriggerPieces { get; }
    }
}
