using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.NSUNet
{
    public partial class Times
    {
        private static long handshakeResponse = 10000;
        public static long HandshakeResponse { get { return handshakeResponse; } set { handshakeResponse = value; } }
    }
}
