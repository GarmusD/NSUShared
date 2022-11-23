namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct RelayModuleSetupContent
    {
        public byte ConfigPos { get; }
        public bool Enabled { get; }
        public bool ActiveLow { get; }
        public bool Reversed { get; }

        public RelayModuleSetupContent(byte configPos, bool enabled, bool activeLow, bool reversed)
        {
            ConfigPos = configPos;
            Enabled = enabled;
            ActiveLow = activeLow;
            Reversed = reversed;
        }
    }
}
