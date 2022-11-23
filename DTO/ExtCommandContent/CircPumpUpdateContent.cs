namespace NSU.Shared.DTO.ExtCommandContent
{
	public struct CircPumpUpdateContent
    {
        public byte ConfigPos { get; }
		public bool Enabled { get; }		
		public string Name { get; }
		public string TempTriggerName { get; }
		public byte MaxSpeed { get; }
		public byte Speed1Channel { get; }
        public byte Speed2Channel { get; }
        public byte Speed3Channel { get; }

        public CircPumpUpdateContent(byte configPos, bool enabled, string name, string tempTriggerName, byte maxSpeed, byte speed1Channel, byte speed2Channel, byte speed3Channel)
		{
			ConfigPos = configPos;
			Enabled = enabled;
			Name = name;
			TempTriggerName = tempTriggerName;
			MaxSpeed = maxSpeed;
			Speed1Channel = speed1Channel;
			Speed2Channel = speed2Channel;
			Speed3Channel = speed3Channel;
		}
    }
}
