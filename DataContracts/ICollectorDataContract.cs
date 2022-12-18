namespace NSU.Shared.DataContracts
{
    public interface ICollectorDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string CircPumpName { get; set; }
        int ActuatorsCount { get; }
        IThermoActuatorDataContract[] Actuators { get; }
    }
}
