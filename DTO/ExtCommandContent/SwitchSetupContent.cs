using NSU.Shared.NSUSystemPart;

namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct SwitchSetupContent
    {
        public byte ConfigPos { get; }
        public bool Enabled { get; }
        public string Name { get; }
        public string DependancyName { get; }
        public Status OnDependancyStatus { get; }
        public Status ForceStatus { get; }
        public Status DefaultStatus { get; }

        public SwitchSetupContent(byte configPos, bool enabled, string name, string depName, Status onDepStatus, Status forceStatus, Status defaultStatus)
        {
            ConfigPos = configPos;
            Enabled = enabled;
            Name = name;
            DependancyName = depName;
            OnDependancyStatus = onDepStatus;
            ForceStatus = forceStatus;
            DefaultStatus = defaultStatus;
        }
    }
}
