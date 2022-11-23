namespace NSU.Shared.DTO.ExtCommandContent
{
	public struct SystemFanSetupContent
    {
		public byte ConfigPos { get; }
		public bool Enabled { get; }
		public string Name { get; }
		public string TempSensorName { get; }
		public double MinTemperature { get; }
		public double MaxTemperature { get; }

		public SystemFanSetupContent(byte configPos, bool enabled, string name, string tempSensorName, double minTemperature, double maxTemperature)
		{
			ConfigPos = configPos;
			Enabled = enabled;
			Name = name;
			TempSensorName = tempSensorName;
			MinTemperature = minTemperature;
			MaxTemperature = maxTemperature;
		}
    }
}
