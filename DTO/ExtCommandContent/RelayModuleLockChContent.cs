namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct RelayModuleLockChContent
    {
        public bool DoLock { get; set; }
        public bool OpenChannel { get; set; }
        public byte Channel { get; set; }

        public RelayModuleLockChContent(bool doLock, byte channel, bool openChannel)
        {
            DoLock = doLock;
            OpenChannel = openChannel;
            Channel = channel;
        }
    }
}
