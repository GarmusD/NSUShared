using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.NSUNet
{
    public interface INetProtocol
    {
        List<byte[]> Encode(string data);
        List<byte[]> Encode(byte[] data);
        INetMessage Decode(byte[] data);
    }
}
