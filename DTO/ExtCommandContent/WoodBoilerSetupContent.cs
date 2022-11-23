namespace NSU.Shared.DTO.ExtCommandContent
{
	public struct WoodBoilerSetupContent
    {
		public byte ConfigPos { get; }
		public string Name { get; }
		public string TempSensorName { get; }
		public string KTypeName { get; }
		public byte LadomatChannel { get; }
		public byte ExhaustFanChannel { get; }
		public double WorkingTemperature { get; }
		public double WorkingHisteresis { get; }
		public double LadomatWorkingTemp { get; }
		public string LadomatTempTriggerName { get; }
		public string WaterBoilerName { get; }

		public WoodBoilerSetupContent(byte configPos, string name, string tempSensorName, string ktypeName, byte ladomatChannel, byte exhaustFanChannel,
			double workingTemperature, double workingHisteresis, double ladomatWorkingTemp, string ladomatTempTriggerName, string waterBoilerName)
		{
			ConfigPos = configPos;
			Name = name;
			TempSensorName = tempSensorName;
			KTypeName = ktypeName;
			LadomatChannel = ladomatChannel;
			ExhaustFanChannel = exhaustFanChannel;
			WorkingTemperature = workingTemperature;
			WorkingHisteresis = workingHisteresis;
			WaterBoilerName = waterBoilerName;
			LadomatWorkingTemp = ladomatWorkingTemp;
			LadomatTempTriggerName = ladomatTempTriggerName;
		}
    }
}
