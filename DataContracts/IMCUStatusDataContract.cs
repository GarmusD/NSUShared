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
        MCUStatus Status { get; set; }
        int FreeMem { get; set; }
        int UpTime { get; set; }
        bool RebootRequired { get; set; }
    }
}
