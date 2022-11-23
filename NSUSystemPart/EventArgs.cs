using NSU.Shared.DataContracts;
using System;

namespace NSU.Shared.NSUSystemPart
{    
    public class NameChangedEventArgs : EventArgs
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
        public NameChangedEventArgs(string newName, string oldName = "")
        {
            NewName = newName;
            OldName = oldName;
        }
    }

    public class McuStatusChangedEventArgs : EventArgs
    {
        public MCUStatus Status { get; set; }
        public McuStatusChangedEventArgs(MCUStatus value)
        {
            Status = value;
        }
    }

    public class StatusChangedEventArgs : EventArgs
    {
        public Status Status { get; set; }
        public StatusChangedEventArgs(Status value)
        {
            Status = value;
        }
    }

    public class SwitchStatusChangedEventArgs : EventArgs
    {
        public Status Status { get; set; }
        public bool IsForced { get; set; }
        public SwitchStatusChangedEventArgs(Status status, bool isForced)
        {
            Status = status;
            IsForced = isForced;
        }
    }

    public class TempChangedEventArgs : EventArgs
    {
        public float Temperature { get; set; }
        public TempChangedEventArgs(float value)
        {
            Temperature = value;
        }
    }

    public class IntervalChangedEventArgs : EventArgs
    {
        public int Interval { get; set; }
        public IntervalChangedEventArgs(int interval)
        {
            Interval = interval;
        }
    }

    public class EnabledChangedEventArgs : EventArgs
    {
        public bool Enabled { get; set; }
        public EnabledChangedEventArgs(bool value)
        {
            Enabled = value;
        }
    }

    public class ActuatorOpenedEventArgs : EventArgs
    {
        public bool Opened { get; set; }
        public ActuatorOpenedEventArgs(bool isOpened)
        {
            Opened = isOpened;
        }
    }

    public class LowTempModeEventArgs : EventArgs
    {
        public bool LowTempMode { get; set; }
        public LowTempModeEventArgs(bool isLowTempMode)
        {
            LowTempMode = isLowTempMode;
        }
    }

    public class BinUploaderProgressEventArgs : EventArgs
    {
        public int Value { get; set; }
        public BinUploaderProgressEventArgs(int value)
        {
            Value = value;
        }
    }

    public class BinUploaderTextMsgEventArgs : EventArgs
    {
        public string Message { get; set; }
        public BinUploaderTextMsgEventArgs(string msg)
        {
            Message = msg;
        }
    }

    public class WoodBoilerStatusChangedEventArgs : EventArgs
    {
        public WoodBoilerStatus Status { get; }
        public WoodBoilerStatusChangedEventArgs(WoodBoilerStatus status)
        {
            Status = status;
        }
    }

    public class SwitchStateChangedEventArgs : EventArgs
    {
        public SwitchTarget Target { get; }
        public SwitchStateChangedEventArgs(SwitchTarget target)
        {
            Target = target;
        }
    }

    public class NSUActionEventArgs : EventArgs
    {
        public NSUAction Action { get; }
        public NSUActionEventArgs(NSUAction action)
        {
            Action = action;
        }
    }
}
