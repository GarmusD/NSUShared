namespace NSU.Shared.DTO.ExtCommandContent
{
	public struct ComfortZoneSetupContent
    {
		public bool Enabled { get; set; }
		public byte ConfigPos { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string RoomTempSensor { get; set; }
		public string FloorTempSensor { get; set; }
		public string Collector { get; set; }
		public double RoomTempHi { get; set; }
		public double RoomTempLo { get; set; }
		public double FloorTemoHi { get; set; }
		public double FloorTempLo { get; set; }
		public double Histerezis { get; set; }
		public byte Actuator { get; set; }
		public bool LowTempMode { get; set; }

		public ComfortZoneSetupContent(byte configPos, bool enabled, string name, string title, string roomTempSensor, string floorTempSensor, string collector,
            double roomTempHi, double roomTempLo, double floorTemoHi, double floorTempLo, double histerezis, byte actuator, bool lowTempMode)
		{
			ConfigPos = configPos;
			Enabled = enabled;
			Name = name;
			Title = title;
			RoomTempSensor = roomTempSensor;
			FloorTempSensor = floorTempSensor;
			Collector = collector;
			RoomTempHi = roomTempHi;
			RoomTempLo = roomTempLo;
			FloorTempLo = floorTempLo;
			FloorTemoHi = floorTemoHi;
			Histerezis = histerezis;
			Actuator = actuator;
			LowTempMode = lowTempMode;
		}
    }
}
