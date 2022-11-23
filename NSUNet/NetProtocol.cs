using System;
using System.Collections.Generic;

namespace NSU.Shared.NSUNet
{
    internal class NetProtocol
    {
        private readonly List<INetProtocol> _netProtocols;
        public NetProtocol()
        {
            _netProtocols = new List<INetProtocol>()
            {
                new NetProtocolV1(),
                new NetProtocolV2()
            };
        }

        public INetMessage? Decode(byte[] data, int protocolVersion)
        {
            return Select(protocolVersion).Decode(data);
        }

        public List<byte[]> Encode(string data, int protocolVersion)
        {
            return Select(protocolVersion).Encode(data);
        }

        public List<byte[]> Encode(byte[] data, int protocolVersion)
        {
            return Select(protocolVersion).Encode(data);
        }

        public List<byte[]> Encode(INetMessage data, int protocolVersion)
        {
            return data.DataType switch
            {
                DataType.String => Select(protocolVersion).Encode(data.AsString()),
                DataType.Bytes => Select(protocolVersion).Encode(data.AsBytes()),
                _ => throw new ArgumentOutOfRangeException(nameof(data.DataType)),
            };
        }

        private INetProtocol Select(int version)
        {
            return _netProtocols[version-1];
        }
    }
}
