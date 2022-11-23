#if !NSUWATCHER
using System;
using System.Collections.Generic;
using System.Linq;
using NSU.Shared.NSUNet;
using NSU.Shared;
using NSUAppShared.NSUSystemParts;
using Newtonsoft.Json.Linq;
using NSU.Shared.NSUTypes;
using NSUAppShared;
using NSU.Shared.NSUXMLConfig;
using Console = NSUAppShared.NSUSystemParts.Console;

namespace NSU.NSUSystem
{

    public class NSUSys
    {
        private const string LogTag = "NSUSys";

        public class NSUSysPartInfo
        {
            public PartTypes PartType;
            public string[] AcceptableCmds;
            public NSUSysPartsBase Part;
        };

        public event EventHandler<EventArgs> OnNSUSystemReady;
        public event EventHandler OnNSUSystemUnavailable;

        private readonly NSUSysPartInfo _sysPart;
        
        /*
         * Properties
        */
        public static NSUSys Instance => _instance;
        public NSUNetwork NSUNetwork { get; private set; }
        public bool NSUSystemReady { get; private set; }
        public NSUXMLConfig XMLConfig { get; private set; }
        public List<NSUSysPartInfo> Parts { get; private set; }
        /*
         * 
         */
        private static NSUSys _instance = null;

        public NSUSys(NSUNetwork network)
        {
            NSUNetwork = network ?? throw new ArgumentNullException(nameof(network), "NSUNetwork cannot be null.");

            //Add NsuNet event handlers
            NSUNetwork.HandshakeReceived += OnHandshakeReceivedHandler;
            NSUNetwork.LoginFailed += OnLoginFailed;
            NSUNetwork.DataReceived += OnClientDataReceivedHandler;
            NSUNetwork.DisconnectedFromServer += OnDisconnectedFromServerHandler;

            XMLConfig = new NSUXMLConfig();
            Parts = CreateParts();
            _sysPart = CreateSysPartInfo(new Syscmd(this, PartTypes.System));
            Parts.Add(_sysPart);
            NSUSystemReady = false;
            _instance = this;
        }

        public void Free()
        {
            NSUSystemReady = false;
            if (NSUNetwork != null)
            {
                NSUNetwork.HandshakeReceived -= OnHandshakeReceivedHandler;
                NSUNetwork.LoginFailed -= OnLoginFailed;
                NSUNetwork.DataReceived -= OnClientDataReceivedHandler;
                NSUNetwork.DisconnectedFromServer -= OnDisconnectedFromServerHandler;
            }
            NSUNetwork.Free();
            NSUNetwork = null;
            Parts.Clear();
            Parts = null;
            XMLConfig.Clear();
            XMLConfig = null;
        }

        private bool DoHashLogin()
        {        
            if (NSUNetwork.Credentials.UseHashForLogin)
            {
                JObject jo = new JObject
                {
                    [JKeys.Generic.Target] = JKeys.Syscmd.TargetName,
                    [JKeys.Generic.Action] = JKeys.SystemAction.Login,
                    [JKeys.Generic.Content] = JKeys.ActionLogin.LoginWithHash,
                    [JKeys.ActionLogin.DeviceID] = NSUNetwork.Credentials.DeviceID,
                    [JKeys.ActionLogin.Hash] = NSUNetwork.Credentials.Hash,
                    [JKeys.UserInfo.UserAccepts] = (int)NetClientAccepts.All
                };
                jo = NSUNetwork.SetRequestAck(jo, "login");
                NSUNetwork.SendCommand(jo);
                return true;
            }
            return false;
        }

        private bool DoCredentialsLogin()
        {
            if (NSUNetwork.Credentials.CredentialsOk)
            {
                JObject jo = new JObject
                {
                    [JKeys.Generic.Target] = JKeys.Syscmd.TargetName,
                    [JKeys.Generic.Action] = JKeys.SystemAction.Login,
                    [JKeys.Generic.Content] = JKeys.ActionLogin.LoginWithCredentials,
                    [JKeys.ActionLogin.UserName] = NSUNetwork.Credentials.UserName,
                    [JKeys.ActionLogin.Password] = NSUNetwork.Credentials.Password,
                    [JKeys.UserInfo.UserAccepts] = (int)NetClientAccepts.All
                };
                jo = NSUNetwork.SetRequestAck(jo, "login");
                NSUNetwork.SendCommand(jo);
                return true;
            }
            return false;
        }        

        private void OnDisconnectedFromServerHandler(object sender, EventArgs e)
        {
            MakeUnavailable();
        }

        private void OnHandshakeReceivedHandler(object sender, EventArgs e)
        {
            if(!DoHashLogin())
                if(!DoCredentialsLogin())
                {
                    //error
                    //TODO Ask for credentials
                    return;
                }
            
        }

        private void OnLoginFailed(object sender, ClientLogintFailureEventArgs e)
        {
            if(e.ErrCode.Equals(JKeys.ErrCodes.Login.InvalidHash))
            {
                if(!DoCredentialsLogin())
                {
                    //error
                    //TODO Ask for credentials
                }
            }
            else if(e.ErrCode.Equals(JKeys.ErrCodes.Login.InvalidUsrNamePassword))
            {
                //TODO Ask for credentials
            }
        }

        void OnClientDataReceivedHandler(object sender, DataReceivedEventArgs e)
        {
            var partInfo = FindPartInfo((string)e.Data[JKeys.Generic.Target]);
            if (partInfo != null)
                partInfo.Part.ParseNetworkData(e.Data);
            else
                NSULog.Debug("HandleOnClientDataReceived2", $"NSUSysPart [{(string)e.Data[JKeys.Generic.Target]}] not found.");
        }

        internal void ReInit()
        {
            MakeUnavailable();
            foreach(var item in Parts) item.Part.Clear();
            (_sysPart.Part as Syscmd).RequestSnapshot();
        }

        internal void MakeReady()
        {
            NSULog.Debug(LogTag, "MakeReady(). Invoking OnNSUPartsReady event.");
            NSUSystemReady = true;
            try
            {
                var evt = OnNSUSystemReady;
                evt?.Invoke(this, EventArgs.Empty);
            }
            catch(Exception ex)
            {
                NSULog.Exception(LogTag, $"OnNSUPartsReady exception: {ex}");
            }
        }

        internal void MakeUnavailable()
        {
            if (NSUSystemReady)
            {
                NSULog.Debug(LogTag, "MakeUnavailable()");
                NSUSystemReady = false;
                var evt = OnNSUSystemUnavailable;
                evt?.Invoke(this, EventArgs.Empty);
            }
        }

        public NSUSysPartsBase GetNSUSysPart(PartTypes type)
        {
            return Parts.FirstOrDefault(x => x.PartType == type)?.Part;
        }

        NSUSysPartInfo FindPartInfo(string cmd)
        {
            return Parts.FirstOrDefault(part => part.AcceptableCmds.Contains(cmd));
        }

        public List<NSUSysPartInfo> CreateParts()
        {
            return new List<NSUSysPartInfo>
            {
                CreateSysPartInfo(new TempSensors(this, PartTypes.TSensors)),
                CreateSysPartInfo(new Switches(this, PartTypes.Switches)),
                CreateSysPartInfo(new RelayModules(this, PartTypes.RelayModules)),
                CreateSysPartInfo(new TempTriggers(this, PartTypes.TempTriggers)),
                CreateSysPartInfo(new CircPumps(this, PartTypes.CircPumps)),
                CreateSysPartInfo(new Collectors(this, PartTypes.Collectors)),
                CreateSysPartInfo(new ComfortZones(this, PartTypes.ComfortZones)),
                CreateSysPartInfo(new KTypes(this, PartTypes.KTypes)),
                CreateSysPartInfo(new WaterBoilers(this, PartTypes.WaterBoilers)),
                CreateSysPartInfo(new WoodBoilers(this, PartTypes.WoodBoilers)),
                CreateSysPartInfo(new BinUploader(this, PartTypes.BinUploader)),
                CreateSysPartInfo(new Console(this, PartTypes.Console))
            };
        }

        private NSUSysPartInfo CreateSysPartInfo(NSUSysPartsBase partBase)
        {
            return new NSUSysPartInfo()
            {
                AcceptableCmds = partBase.RegisterTargets(),
                PartType = partBase.PartType,
                Part = partBase
            };
        }
    }
}

#endif