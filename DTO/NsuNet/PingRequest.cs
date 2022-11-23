using NSUWatcher.Interfaces.MCUCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DTO.NsuNet
{
    public class PingRequest : NsuNetRequestBase
    {
        public PingRequest()
        {
            Target = JKeys.Syscmd.TargetName;
            Action = JKeys.SystemAction.Ping;
        }
    }
}
