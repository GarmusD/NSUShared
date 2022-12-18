using NSU.Shared.DataContracts;
using NSU.Shared.NSUSystemPart;
using System;
using System.Linq;

namespace NSU.Shared.DTO.ExtCommandContent
{
	public struct CollectorUpdateContent
    {
		public struct Actuator
		{
			public ActuatorType Type { get; set; }
			public byte Channel { get; set; }

			public Actuator(ActuatorType type, byte channel)
			{
				Type = type;
				Channel = channel;
			}
		}

		public byte ConfigPos { get; }
		public bool Enabled { get; }		
		public string Name { get; }
		public string CircPumpName { get; }
		public byte ActuatorsCount { get; }
		public Actuator[] Actuators { get; private set; }//MAX_COLLECTOR_VALVES

		public CollectorUpdateContent(byte configPos, bool enabled, string name, string circPumpName, byte actuatorsCount, params Actuator[] actuators)
		{
            if (actuators.Length > Collector.MaxCollectorActuators)
                throw new ArgumentOutOfRangeException(nameof(actuators), $"Count of actuators [{actuators.Length}] exceeds maximum value [{Collector.MaxCollectorActuators}].");

            Actuators = Enumerable.Range(0, Collector.MaxCollectorActuators).Select(_ => new Actuator(ActuatorType.NC, 0xFF)).ToArray();
			ConfigPos = configPos;
			Enabled = enabled;
			Name = name;
			CircPumpName = circPumpName;
			ActuatorsCount = actuatorsCount;
			int idx = 0;
			
			foreach (var item in actuators)
			{
				Actuators[idx].Type = item.Type;
				Actuators[idx].Channel = item.Channel;
				idx++;
			}
		}
	}
}
