namespace NSU.Shared.NSUNet
{
    public enum DataType
    {
        String,
        Bytes
    }

    public interface INetMessage
    {
        public DataType DataType { get; }
        public object Data { get; }
        public string AsString();
        public byte[] AsBytes();
    }
}
