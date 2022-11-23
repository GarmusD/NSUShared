namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct KTypeSetupContent
    {
        public byte ConfigPos { get; }
        public bool Enabled { get; }
        public string Name { get; }
        public int Interval { get; }

        public KTypeSetupContent(byte configPos, bool enabled, string name, int interval)
        {
            ConfigPos = configPos;
            Enabled = enabled;
            Name = name;
            Interval = interval;
        }
    }
}
