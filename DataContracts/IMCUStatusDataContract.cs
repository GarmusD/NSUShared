using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public enum MCUStatus
    {
        Off,
        Booting,
        BootPauseReady,
        BootPaused,
        Running
    }

    public interface IMCUStatusDataContract : INSUSysPartDataContract
    {
        public MCUStatus Status { get; set; }
        public int FreeMem { get; set; }
        public int UpTime { get; set; }
        public bool RebootRequired { get; set; }
    }
}
