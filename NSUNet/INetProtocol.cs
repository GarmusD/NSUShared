using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.NSUNet
{
    public interface INetProtocol
    {
        public List<byte[]> Encode(string data);
        public List<byte[]> Encode(byte[] data);
        public INetMessage? Decode(byte[] data);
    }
}
