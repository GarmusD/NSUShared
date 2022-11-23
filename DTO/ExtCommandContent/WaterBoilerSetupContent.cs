using System.Linq;

namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct WaterBoilerSetupContent
    {
        public byte ConfigPos { get; }
        public bool Enabled { get; }
        public string Name { get; }
        public string TempSensorName { get; }
        public string TempTriggerName { get; }
        public string CircPumpName { get; }
        public bool ElHeatingEnabled { get; }
        public byte ElPowerChannel { get; }
        public ElHeatingTime[] HeatingTime { get; }

        public WaterBoilerSetupContent(byte configPos, bool enabled, string name, string tempSensorName, string tempTriggerName,
            string circPumpName, bool elHeatingEnabled, byte elPowerChannel, params ElHeatingTime[] elHeatingTime)
        {
            ConfigPos = configPos;
            Enabled = enabled;
            Name = name;
            TempSensorName = tempSensorName;
            TempTriggerName = tempTriggerName;
            CircPumpName = circPumpName;
            ElHeatingEnabled = elHeatingEnabled;
            ElPowerChannel = elPowerChannel;            
            HeatingTime = Enumerable.Range(0, 7).Select(_ =>  new ElHeatingTime(0xFF, 0xFF, 0xFF, 0xFF)).ToArray();
            int idx = 0;
            foreach (var item in elHeatingTime)
            {
                HeatingTime[idx++] = item;
            }
        }

        public struct ElHeatingTime
        {
            public byte StartHour { get; internal set; }
            public byte StartMinute { get; internal set; }
            public byte StopHour { get; internal set; }
            public byte StopMinute { get; internal set; }

            public ElHeatingTime(byte startHour, byte startMinute, byte stopHour, byte stopMinute)
            {
                StartHour = startHour;
                StartMinute = startMinute;
                StopHour = stopHour;
                StopMinute = stopMinute;
            }
        }
    }


}
