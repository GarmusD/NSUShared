#if !NSUWATCHER
using System;
using System.Threading.Tasks;

namespace NSU.Shared.NSUNet
{
    /// <summary>
    /// Interface to wrap Win/Adroid network
    /// </summary>
    public interface INSUNetworkSocket
    {
        event EventHandler<EventArgs> ConnectStarted; 
        event EventHandler<EventArgs> Connected;
        event EventHandler<EventArgs> Disconnected;
        event EventHandler<EventArgs> ConnectTimeout;
        event EventHandler<EventArgs> ConnectFailed;
        event EventHandler<NSUSocketDataReceivedEventArgs> DataReceived;
        event EventHandler<EventArgs> InvalidHost;
        
        bool IsConnected { get; }
        bool IsConnecting { get; }

        Task ConnectAsync(string ipOrHost, int port);
        void Disconnect(bool broadcast=true);
        
        Task SendDataAsync(byte[] buf, int offset=0, int cnt=0);
    }
}
#endif