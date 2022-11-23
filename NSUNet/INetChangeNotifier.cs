#if !NSUWATCHER
using System;

namespace NSU.Shared.NSUNet
{
    public enum NetworkType
    {
        None,
        Mobile,
        Wifi,
        Ethernet
    };

    public sealed class NetChangedEventArgs
    {
        public bool NetAvailable{ get; }
        public NetworkType NetType { get; }

        public NetChangedEventArgs(bool value, NetworkType netType)
        {
            NetAvailable = value;
            NetType = netType;
        }
    }

    public interface INetChangeNotifier
    {
        event EventHandler<NetChangedEventArgs> OnNetworkAvailableChange;
        bool IsOnline { get; }
        NetworkType NetType { get; }
    }
}

#endif