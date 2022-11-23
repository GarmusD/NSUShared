using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.NSUNet
{
    [Flags]
    public enum NetClientAccepts
    {
        None = 0,
        System = 1,
        Debug = 1 << 1,
        Warning = 1 << 2,
        Error = 1 << 3,
        Alarm = 1 << 4,
        Object = 1 << 5,
        Info = 1 << 6,
        Status = 1 << 7,
        Update = 1 << 8,
        All = System | Debug | Warning | Error | Alarm | Object | Info | Status | Update
    };
}
