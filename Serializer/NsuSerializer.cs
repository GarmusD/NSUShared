using Newtonsoft.Json;

namespace NSU.Shared.Serializer
{
    public class NsuSerializer : INsuSerializer
    {
        private JsonSerializerSettings _settings;

        public NsuSerializer()
        {
            _settings = LowercaseNamingStrategy.LowercaseSettings;
        }

        public T? Deserialize<T>(string json) where T : struct
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }
    }
}
