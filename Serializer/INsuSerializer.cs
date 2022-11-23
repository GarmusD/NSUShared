using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.Serializer
{
    public interface INsuSerializer
    {
        public string Serialize(object obj);
        public T? Deserialize<T>(string json) where T : struct;
    }
}
