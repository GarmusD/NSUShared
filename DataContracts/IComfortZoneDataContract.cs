using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NSU.Shared.DataContracts
{
    public interface IComfortZoneDataContract : INSUSysPartDataContract
    {
        int ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        string RoomSensorName { get; set; }
        string FloorSensorName { get; set; }
        string CollectorName { get; set; }
        double RoomTempHi { get; set; }
        double RoomTempLow { get; set; }
        double FloorTempHi { get; set; }
        double FloorTempLow { get; set; }
        double Histeresis { get; set; }
        int Actuator { get; set; }
        bool LowTempMode { get; set; }
        double CurrentRoomTemperature { get; set; }
        double CurrentFloorTemperature { get; set; }
        bool ActuatorOpened { get; set; }
    }
}
