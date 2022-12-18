namespace NSU.Shared
{
    public class JKeys
    {
        public static class Generic
        {
            public const string CommandName = "cmdname";
            public const string Name = "name";
            public const string ConfigPos = "cfgpos";
            public const string CommandID = "cmdid";
            public const string Target = "target";
            public const string Source = "src";
            public const string Action = "action";
            public const string Content = "content";
            public const string Result = "result";
            public const string ErrCode = "errcode";
            public const string Error = "error";
            public const string Message = "msg";
            public const string Value = "value";
            public const string Status = "status";
            public const string Enabled = "enabled";
            public const string ResponseRequired = "respreq";
            public const string Setup = "setup";
        }

        public static class Content
        {
            public const string Object = "object";
            public const string Info = "info";
            public const string Config = "config";
            public const string ConfigPlus = "configplus";
        }

        public static class Action
        {
            public const string Info = "info";
            public const string Status = "status";
            public const string Error = "error";
            public const string Get = "get";
            public const string Set = "set";
            public const string Setup = "setup";
            public const string Update = "update";
            public const string Clear = "clear";
            public const string Click = "click";
            public const string Snapshot = "snapshot";
        }

        public static class Result
        {
            public const string Ok = "ok";
            public const string Error = "error";
            public const string Null = "null";
            public const string Done = "done";
        }

        public class Alarm
        {
            public const string TargetName = "alarm";
            public const string Temp = "temp";
            public const string Histeresis = "hist";
            public const string ChannelData = "chdata";
            public const string Channel = "ch";
            public const string IsOpen = "open";
        }

        public static class Syscmd
        {
            public const string TargetName = "system";
            public const string SystemStatus = "systemstatus";
            public const string RebootSystem = "reboot";
            public const string RebootRequired = "rebootreq";
            public const string ReadyPauseBoot = "readypauseboot";
            public const string SystemBootPaused = "bootpaused";
            public const string PauseBoot = "pauseboot";
            public const string SystemBooting = "booting";
            public const string SystemRunning = "running";
            public const string Snapshot = "snapshot";
            public const string FreeMem = "freemem";
            public const string UpTime = "uptime";
            public const string SetTime = "settime";
            public const string Year = "y";
            public const string Month = "m";
            public const string Day = "d";
            public const string Hour = "h";
            public const string Minute = "mm";
            public const string Second = "s";
            public const string TimeChanged = "timechanged";
        }

        public static class BinUploader
        {
            public const string TargetName = "binuploader";
            public const string StartUpload = "startupload";
            public const string Abort = "abort";
            public const string Data = "data";
            public const string DataDone = "datadone";
            public const string StartFlash = "startflash";
            public const string FlashStarted = "flashstarted";
            public const string VerifyStarted = "verifystarted";
            public const string FlashDone = "flashdone";
            public const string Progress = "progress";
            public const string InfoText = "info";
        }

        public static class CircPump
        {
            public const string TargetName = "cpump";
            public const string Enabled = "enabled";
            public const string ActionClick = "click";
            public const string MaxSpeed = "maxspd";
            public const string Speed1Ch = "spd1";
            public const string Speed2Ch = "spd2";
            public const string Speed3Ch = "spd3";
            public const string TempTriggerName = "trgname";
            public const string CurrentSpeed = "speed";
            public const string ValvesOpened = "vopened";
            public const string DbId = "dbid";
        }

        public static class Collector
        {
            public const string TargetName = "collector";
            public const string Enabled = "enabled";
            public const string Valves = "valves";
            public const string Valve = "valve";
            public const string CircPumpName = "cpname";
            public const string ActuatorsCount = "vcnt";
            public const string ActuatorType = "type";
            public const string ActuatorChannel = "ch";
            public const string ActuatorOpened = "opened";
        }

        public static class ComfortZone
        {
            public const string TargetName = "czone";
            public const string Title = "title";
            public const string Enabled = "enabled";
            public const string RoomSensorName = "rsname";
            public const string FloorSensorName = "fsname";
            public const string CollectorName = "clname";
            public const string RoomTempHi = "rth";
            public const string RoomTempLow = "rtl";
            public const string FloorTempHi = "fth";
            public const string FloorTempLow = "ftl";
            public const string Histeresis = "hist";
            public const string Actuator = "act";
            public const string CurrentRoomTemp = "crt";
            public const string CurrentFloorTemp = "cft";
            public const string ActuatorOpened = "actopened";
            public const string LowTempMode = "lowmode";
        }

        public static class Switch
        {
            public const string TargetName = "switch";
            public const string Enabled = "enabled";
            public const string Dependancy = "dname";
            public const string OnDependancyStatus = "depstate";
            public const string ForceStatus = "forcestate";
            public const string IsForced = "isforced";
            public const string CurrState = "currstate";
        }

        public static class TempSensor
        {
            public const string TargetName = "tsensor";
            public const string ContentSystem = "system";
            public const string ContentConfig = "config";
            public const string SensorID = "addr";
            public const string Temperature = "temp";
            public const string Enabled = "enabled";
            public const string Interval = "interval";
            public const string ReadErrors = "errors";
        }

        public static class TempTrigger
        {
            public const string TargetName = "trigger";
            public const string Enabled = "enabled";
            public const string Status = "status";
            public const string Pieces = "pieces";
            public const string TriggerCondition = "cndt";
            public const string TempSensorName = "sname";
            public const string TriggerTemperature = "temp";
            public const string TriggerHisteresis = "hist";
        }

        public static class RelayModule
        {
            public const string TargetName = "relay";
            public const string ActionOpenChannel = "open";
            public const string ActionCloseChannel = "close";
            public const string ActionLockChannel = "lock";
            public const string ActionUnlockChannel = "unlock";
            public const string ActiveLow = "activelow";
            public const string Reversed = "reversed";
            public const string StatusFlags = "sflags";
            public const string LockFlags = "lflags";
            public const string IsLocked = "locked";
        }

        public static class KType
        {
            public const string TargetName = "ktype";
            public const string Enabled = "enabled";
            public const string Interval = "interval";
            public const string Temperature = "temp";
        }

        public static class WaterBoiler
        {
            public const string TargetName = "waterboiler";
            public const string TempSensorName = "tsname";
            public const string CircPumpName = "cpname";
            public const string TempTriggerName = "trgname";
            public const string ElPowerChannel = "powerch";
            public const string ElHeatingEnabled = "elhenabled";
            public const string PowerData = "powerdata";
            public const string PDStartHour = "starth";
            public const string PDStartMin = "startm";
            public const string PDStopHour = "stoph";
            public const string PDStopMin = "stopm";
        }

        public static class WoodBoiler
        {
            public const string TargetName = "woodboiler";
            public const string Enabled = "enabled";
            public const string TSensorName = "tsname";
            public const string KTypeName = "ktpname";
            public const string LadomChannel = "lch";
            public const string LadomIsOn = "lison";
            public const string LadomIsManual = "lism";
            public const string LadomatStatus = "lstatus";
            public const string ExhaustFanChannel = "smokech";
            public const string ExhaustFanStatus = "exhstatus";
            public const string VentIsOn = "vison";
            public const string VentIsManual = "vism";
            public const string WorkingTemp = "worktemp";
            public const string Histeresis = "hist";
            public const string Status = "status";
            public const string CurrentTemp = "currtemp";
            public const string LadomatWorkTemp = "ltemp";
            public const string LadomatTriggerName = "ltrgname";
            public const string TemperatureStatus = "tstatus";
            public const string TemperatureChange = "temp";
            public const string WaterBoilerName = "wbname";

            public const string ActionIkurimas = "ikurimas";
            public const string ActionSwitch = "switch";

            public const string TargetExhaustFan = "exhaust";
            public const string TargetLadomat = "ladomat";
        }

        public static class SystemFan
        {
            public const string TargetName = "sysfan";
            public const string TSensorName = "tsname";
            public const string MinTemp = "mint";
            public const string MaxTemp = "maxt";
            public const string CurrentPWM = "cpwm";
        }

        public static class Console
        {
            public const string TargetName = "console";
            public const string ConsoleOut = "output";
            public const string Start = "start";
            public const string Stop = "stop";
            public const string ManualCommand = "mancmd";
        }

        public static class SystemAction
        {
            public const string Login = "login";
            public const string Handshake = "handshake";
            public const string Ping = "ping";
            public const string PushID = "pushid";
            public const string Ready = "ready";
        }

        public static class Timer
        {
            public const string ActionTimer = "timer";
        }

        public static class UserCmd
        {
            public const string TargetName = "usercmd";
            public const string ActionExec = "exec";
        }

        public static class ActionLogin
        {
            public const string LoginWithCredentials = "credentials";
            public const string LoginWithHash = "hash";
            public const string UserName = "usrname";
            public const string Password = "password";
            public const string DeviceID = "deviceid";
            public const string Hash = "hash";

            public const string NeedChangePassword = "changepswd";
        }

        public static class UserInfo
        {
            public const string UserAccepts = "accept";
        }

        public static class ServerInfo
        {
            public const string Name = "name";
            public const string Version = "version";
            public const string Protocol = "protocol";
        }

        public static class ErrCodes
        {
            public static class Login
            {
                public const string InvalidUsrNamePassword = "invalid_pswd";
                public const string InvalidHash = "invalid_hash";
            }

            public static class BinUploader
            {
                public const string FileCreateError = "file_create_error";
                public const string BossacError = "bossac_error";
                public const string FlashInProgress = "flash_in_progress";

                public const string Error = "error";
            }
        }
    }
}

