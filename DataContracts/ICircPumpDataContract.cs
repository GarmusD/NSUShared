using NSU.Shared.NSUSystemPart;

namespace NSU.Shared.DataContracts
{
    public interface ICircPumpDataContract : INSUSysPartDataContract
    {
        byte ConfigPos { get; set; }
        bool Enabled { get; set; }
        string Name { get; set; }
        string TempTriggerName { get; set; }
        byte CurrentSpeed { get; set; }
        byte MaxSpeed { get; set; }
        byte Spd1Channel { get; set; }
        byte Spd2Channel { get; set; }
        byte Spd3Channel { get; set; }
        Status Status { get; set; }
        int OpenedValvesCount { get; set; }
    }
}
