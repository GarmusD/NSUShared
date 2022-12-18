namespace NSU.Shared.NSUNet
{
    public enum DataType
    {
        String,
        Bytes
    }

    public interface INetMessage
    {
        DataType DataType { get; }
        object Data { get; }
        string AsString();
        byte[] AsBytes();
    }
}
