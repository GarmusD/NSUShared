using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.DataContracts
{
    public interface IAlarmDataContract : INSUSysPartDataContract
    {
        int ConfigPos { get; set; }
        IAlarmChannelInfo[] ChannelInfo { get; set; }
        double AlarmTemp { get; set; }
        double Histersis { get; set; }
        bool IsAlarm { get; set; }
    }

    public interface IAlarmChannelInfo
    {
        int Channel { get; set; }
        bool Opened { get; set; }
    };
}
