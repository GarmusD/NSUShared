using System;

namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct TempSensorSetupContent
    {
        public byte ConfigPos { get; }
        public bool Enabled { get; }
        public byte[] SensorAddress { get; }
        public string Name { get; }

        public TempSensorSetupContent(byte configPos, bool enabled, byte[] sensorAddress, string name)
        {
            if (sensorAddress.Length != 8) throw new ArgumentOutOfRangeException(nameof(sensorAddress), "Sensor address is not valid.");

            ConfigPos = configPos;
            Enabled = enabled;
            SensorAddress = sensorAddress;
            Name = name;
        }
    }
}
