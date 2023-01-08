#if !NSUWATCHER
using System;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NSU.Shared.Compress;
using System.IO;
using System.Threading.Tasks;
using Serilog;

#if __ANDROID__

#endif

namespace NSU.Shared.NSUNet
{
    public enum NetworkStatus
    {
        Disconnected,
        Connecting,
        Connected
    };

    public class NSUNetwork
    {
        public event EventHandler<NetChangedEventArgs> NetworkAvailabilityChanged;
        public event EventHandler<EventArgs> ConnectStarted;
        public event EventHandler<EventArgs> ConnectFailure;
        public event EventHandler<EventArgs> ConnectToServerTimeout;
        public event EventHandler<NetAttemptReconnectEventArgs> ReconectAttempted;
        public event EventHandler<EventArgs> DisconnectedFromServer;
        public event EventHandler<EventArgs> HandshakeReceived;
        public event EventHandler<EventArgs> LoggedIn;
        public event EventHandler<ClientLogintFailureEventArgs> LoginFailed;
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        private readonly ILogger _logger;
        public Credentials Credentials => _credentials;
        public INetChangeNotifier NetChangeNotifier { set => SetNSUNetChangeNotifier(value); }
        public bool NetworkAvailable => GetNetworkAvailable();
        public bool Connected => _iSocket.IsConnected;


        private readonly NSUTimer _pingTimer;
        private readonly INSUNetworkSocket _iSocket;
        private readonly AutoReconnect _autoReconnect;
        private readonly InternalArgBuilder _argbuilder;
        private readonly NSUNetQueue _queue;
        private INetChangeNotifier _netChangeNotifier;
        private readonly Credentials _credentials;


        public NSUNetwork(INSUNetworkSocket nsusocket, INetChangeNotifier netChangeNotifier, Credentials credentials)
        {
            _iSocket = nsusocket ?? throw new ArgumentNullException(nameof(nsusocket), "Value of INSUNetworkSocket cannot be null");
            _netChangeNotifier = netChangeNotifier ?? throw new ArgumentNullException(nameof(netChangeNotifier), "INetChangeNotifier cannot be null.");
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials), "Credentials cannot be null");

            _logger = Log.Logger.ForContext<NSUNetwork>(true);

            _iSocket.ConnectStarted += HandleOnConnectStart;
            _iSocket.Connected += HandleOnConnected;
            _iSocket.ConnectFailed += HandleOnConnectFailed;
            _iSocket.ConnectTimeout += HandleOnConnectTimeout;
            _iSocket.Disconnected += HandleOnDisconnected;
            _iSocket.DataReceived += HandleOnDataReceived;
            _iSocket.InvalidHost += HandleOnInvalidHost;

            _pingTimer = new NSUTimer(1000 * 60 * 5);
            _pingTimer.OnNSUTimer += HandleOnPingTimer;

            _autoReconnect = new AutoReconnect();
            _autoReconnect.OnDoReconnect += HandleOnDoReconnect;
            _autoReconnect.Enabled = NetworkAvailable;

            _argbuilder = new InternalArgBuilder();

            _queue = new NSUNetQueue();
            _queue.OnCommandAvailable += HandleOnCommandAvailable;
            _queue.OnResponseTimeout += QueueOnResponseTimeoutHandler;
        }

        public void Free()
        {
            _pingTimer.Stop();
            Disconnect();
            _iSocket.ConnectStarted -= HandleOnConnectStart;
            _iSocket.Connected -= HandleOnConnected;
            _iSocket.ConnectFailed -= HandleOnConnectFailed;
            _iSocket.ConnectTimeout -= HandleOnConnectTimeout;
            _iSocket.Disconnected -= HandleOnDisconnected;
            _iSocket.DataReceived -= HandleOnDataReceived;
            _iSocket.InvalidHost -= HandleOnInvalidHost;

            _autoReconnect.Enabled = false;
            _autoReconnect.OnDoReconnect -= HandleOnDoReconnect;

            _argbuilder.Clear();
            _queue.Clear();
            _queue.OnCommandAvailable -= HandleOnCommandAvailable;
            _queue.OnResponseTimeout -= QueueOnResponseTimeoutHandler;

            if (_netChangeNotifier != null)
            {
                _netChangeNotifier.OnNetworkAvailableChange -= HandleOnNetworkAvailableChange;
            }
            _netChangeNotifier = null;
        }

        private void SetNSUNetChangeNotifier(INetChangeNotifier value)
        {
            if (_netChangeNotifier != null)
            {
                _netChangeNotifier.OnNetworkAvailableChange -= HandleOnNetworkAvailableChange;
            }
            _netChangeNotifier = value;
            if (_netChangeNotifier != null)
            {
                _netChangeNotifier.OnNetworkAvailableChange += HandleOnNetworkAvailableChange;
            }
        }

        private void HandleOnInvalidHost(object sender, EventArgs e)
        {
            _logger.Debug("Socket raised InvalidHost event.");
            var evt = ConnectFailure;
            evt?.Invoke(this, EventArgs.Empty);
        }

        private void QueueOnResponseTimeoutHandler()
        {
            _logger.Debug("QueueOnResponseTimeoutHandler() - no response to command over time.");
            _iSocket.Disconnect();
            _argbuilder.Clear();
            RaiseOnClientDisconnected();
        }

        void HandleOnDoReconnect(int count)
        {
            var evt = ReconectAttempted;
            evt?.Invoke(this, new NetAttemptReconnectEventArgs
            {
                ReconnectCount = count
            });
            _ = ConnectAsync();
        }

        private void HandleOnConnectStart(object sender, EventArgs e)
        {
            Connecting = true;
            ConnectStarted?.Invoke(null, null);
        }

        void HandleOnDataReceived(object sender, NSUSocketDataReceivedEventArgs e)
        {
            ResetPinger();
            ProccessNetworkData(e.Buffer, e.Count);
        }

        void HandleOnDisconnected(object sender, EventArgs e)
        {
            _logger.Debug("HandleOnDisconnected ()");
            DC();
            RaiseOnClientDisconnected();
            if (NetworkAvailable)
                _autoReconnect.StartReconnect();
        }

        void HandleOnConnectTimeout(object sender, EventArgs e)
        {
            _logger.Debug("HandleOnConnectTimeout ()");
            Connecting = false;
            _pingTimer.Stop();
            _argbuilder.Clear();
            RaiseOnClientConnectTimeout();
            _autoReconnect.StartReconnect();
        }

        void HandleOnConnectFailed(object sender, EventArgs e)
        {
            _logger.Debug("HandleOnConnectFailed ()");
            Connecting = false;
            _argbuilder.Clear();
            _pingTimer.Stop();
            RaiseOnClientConnectFailure();
            _autoReconnect.StartReconnect();
        }

        void HandleOnConnected(object sender, EventArgs e)
        {
            _logger.Debug("HandleOnConnected()");
            Connecting = false;
            _argbuilder.Clear();
            _autoReconnect.StopReconnect();
            _logger.Debug("HandleOnConnected() - Sending handshake");
            JObject jo = new JObject
            {
                [JKeys.Generic.Target] = JKeys.Syscmd.TargetName,
                [JKeys.Generic.Action] = JKeys.SystemAction.Handshake
            };
            jo = SetRequestAck(jo, "handshake");
            _queue.Clear();
            SendCommand(jo);
        }

        void HandleOnNetworkAvailableChange(object sender, NetChangedEventArgs e)
        {
            _logger.Debug(string.Format("HandleOnNetworkAvailableChange ({0})", e.NetAvailable));
            NetworkAvailabilityChanged?.Invoke(this, e);
            if (e.NetAvailable)
            {
                _autoReconnect.Enabled = true;
                _ = ConnectAsync();
            }
            else
            {
                _autoReconnect.Enabled = false;
                Disconnect();
            }
        }

        void HandleOnPingTimer()
        {
            _logger.Debug("HandleOnPingTimer()");
            _pingTimer.Stop();
            JObject jo = new JObject
            {
                [JKeys.Generic.Target] = JKeys.Syscmd.TargetName,
                [JKeys.Generic.Action] = "ping"
            };
            SendString(JsonConvert.SerializeObject(jo));
        }
        
        void ResetPinger()
        {
            _pingTimer.Reset();
        }

        internal void DC()
        {
            _logger.Debug("DC() - clearing internal data.");
            _argbuilder.Clear();
            _pingTimer.Stop();
            _queue.Clear();
        }

        private bool GetNetworkAvailable()
        {
            if (_netChangeNotifier == null)
                return false;
            return _netChangeNotifier.IsOnline;
        }


        public async Task ConnectAsync()
        {
            _logger.Debug("ConnectAsync()");
            if (Connected) 
                Disconnect();

            if (NetworkAvailable)
            {
                await _iSocket.ConnectAsync(_credentials.Host, _credentials.Port);
            }
            else
            {
                _logger.Debug("ConnectAsync() Network not available.");
            }
        }

        public void Disconnect()
        {
            _logger.Debug("Disconnect(). Calling socket.Disconnect()");
            _iSocket.Disconnect();
            _autoReconnect.StopReconnect();
            DC();
        }

        public bool Connecting { get; private set; } = false;

        public void SendCommand(JObject cmd)
        {
            _logger.Debug($"SendCommand(): cmd: {cmd.ToString()}");
            if (Connected)
            {
                _queue.Add(cmd);
            }
        }

        public JObject SetRequestAck(JObject jo, string cmdID)
        {
            if (!string.IsNullOrEmpty(cmdID))
            {
                jo[JKeys.Generic.CommandID] = cmdID;
                jo[JKeys.Generic.ResponseRequired] = true;
            }
            return jo;
        }

        void HandleOnCommandAvailable(JObject cmd)
        {
            SendString(JsonConvert.SerializeObject(cmd));
        }

        public void SendString(string jstr)
        {
                try
                {
                _logger.Debug("SendString: " + jstr);
                    var compressed = StringCompressor.Zip(jstr);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.WriteByte((byte)NetDataType.CompressedString);
                        ms.Write(BitConverter.GetBytes(compressed.Length), 0, sizeof(int));
                        ms.Write(compressed, 0, compressed.Length);
                        ms.Flush();
                        var cbuff = ms.ToArray();
                        if (cbuff != null)
                        {
                            _iSocket.SendDataAsync(cbuff);
                            ResetPinger();
                        }
                    }
                }
                catch (Exception e)
                {
                _logger.Debug("SendString error: " + e.Message);
                }
        }

        public async Task SendUncompressedString(string jstr)
        {
                try
                {
                    var compressed = Encoding.ASCII.GetBytes(jstr);
                    byte[] cbuff = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.WriteByte((byte)NetDataType.String);
                        ms.Write(BitConverter.GetBytes(compressed.Length), 0, sizeof(int));
                        ms.Write(compressed, 0, compressed.Length);
                        cbuff = ms.ToArray();
                    }
                    if (cbuff != null)
                    {
                        await _iSocket.SendDataAsync(cbuff);
                        ResetPinger();
                    }
                }
                catch (Exception e)
                {
                _logger.Debug("SendString error: " + e.Message);
                }
        }

        public async Task SendBuffer(byte[] b, int start = 0, int count = -1)
        {
            int cnt = count == -1 ? b.Length : count;
            using (MemoryStream ms = new MemoryStream())
            {
                ms.WriteByte((byte)NetDataType.Binary);
                ms.Write(BitConverter.GetBytes(cnt), 0, sizeof(int));
                ms.Write(b, start, cnt);
                var buff = ms.ToArray();
                if (buff != null)
                {
                    await _iSocket.SendDataAsync(buff);
                    ResetPinger();
                }
            }
        }

        internal void RaiseOnLoggedInEvent()
        {
            var evt = LoggedIn;
            evt?.Invoke(this, new EventArgs());
        }

        internal void RaiseOnLoginFailedEvent(string errCode)
        {
            var evt = LoginFailed;
            evt?.Invoke(this, new ClientLogintFailureEventArgs(errCode));
        }

        private void RaiseOnClientConnectTimeout()
        {
            var evt = ConnectToServerTimeout;
            evt?.Invoke(this, new EventArgs());
            _autoReconnect.StartReconnect();
        }

        private void RaiseOnClientConnectFailure()
        {
            var evt = ConnectFailure;
            evt?.Invoke(this, new EventArgs());
        }

        internal void RaiseOnHandshakeReceived()
        {
            var evt = HandshakeReceived;
            evt?.Invoke(this, new EventArgs());
        }

        private void RaiseOnClientDisconnected()
        {
            var evt = DisconnectedFromServer;
            evt?.Invoke(this, new EventArgs());
            _autoReconnect.StartReconnect();
        }

        void ProccessNetworkData(byte[] buf, int count)
        {
            _logger.Debug($"ProccessNetworkData() - Data count: {count}");
            if (count > 0)
            {
                try
                {
                    ResetPinger();
                    _argbuilder.Process(buf, 0, count);
                    while (_argbuilder.DataAvailable)
                    {
                        InternalArgs args = _argbuilder.GetArgs();

                        if (args != null)
                        {
                            if (args.DataType == NetDataType.String)
                            {
                                string resp_str = args.Data as string;
                                _logger.Debug("ProccessNetworkData(): " + resp_str);
                                if (!string.IsNullOrWhiteSpace(resp_str))
                                {
                                    if (resp_str.StartsWith("{"))
                                    {
                                        _logger.Debug("Raising OnClientDataReceived event.");
                                        try
                                        {
                                            JObject jo = (JObject)JsonConvert.DeserializeObject(resp_str);
                                            _queue.ResponseReceived(jo);
                                            DataReceived?.Invoke(this, new DataReceivedEventArgs(jo));
                                        }
                                        catch (Exception ex)
                                        {
                                            _logger.Error(ex, "ProccessNetworkData(): Exception: {ex}");
                                        }
                                    }
                                }
                                resp_str = string.Empty;
                            }
                            else if (buf[0] == (byte)NetDataType.Binary)
                            {

                            }
                            else
                            {
                                throw new Exception("Not a NSUServer.");
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    //ivyko fignia kazkokia
                    _logger.Debug("In Network runner: " + e.Message);
                    Disconnect();
                    _autoReconnect.StartReconnect();
                }
            }
        }
    }
}

#endif