namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct RelayModuleOpenChContent
    {
        public bool DoOpen { get; set; }
        public byte Channel { get; set; }

        public RelayModuleOpenChContent(bool doOpen, byte channel)
        {
            DoOpen = doOpen;
            Channel = channel;
        }
    }
}
