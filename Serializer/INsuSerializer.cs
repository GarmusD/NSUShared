namespace NSU.Shared.Serializer
{
    public interface INsuSerializer
    {
        string Serialize(object obj);
        T? Deserialize<T>(string json) where T : struct;
    }
}
