#if !NSUWATCHER
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NSU.Shared.NSUTypes;
using System.Threading.Tasks;

namespace NSU.Shared.NSUNet
{

    public class NSUNetQueue
    {
        const string LogTag = "NSUNetQueue";
        public delegate void CommandAvailableHandler(JObject cmd);
        public delegate void ResponseReceiveTimeoutHandler();

        public event CommandAvailableHandler OnCommandAvailable;
        public event ResponseReceiveTimeoutHandler OnResponseTimeout;

        readonly Queue<JObject> queue;
        JObject current;
        string currentCmdID;
        //bool paused;
        NSUTimer timer;
        readonly object lck = new object();


        public NSUNetQueue()
        {
            queue = new Queue<JObject>();
            timer = new NSUTimer(15000);
            timer.OnNSUTimer += OnCommandResponseTimerHandler;
            currentCmdID = string.Empty;
        }

        void OnCommandResponseTimerHandler()
        {
            NSULog.Debug(LogTag, $"OnCommandResponseTimerHandler().  currentCmdID: '{currentCmdID}'.");
            timer.Stop();
            OnResponseTimeout?.Invoke();
        }

        private void RaiseCurrentAsync()
        {
            var c = current;
            current = null;
            NSULog.Debug(LogTag, "RaiseCurrent() in async mode.");
            Task.Run(() =>
            {
                if (c != null)
                {
                    OnCommandAvailable?.Invoke(c);
                }
            });
        }

        public void Add(JObject cmd)
        {
            NSULog.Debug(LogTag, "Add(cmd)");
            if (cmd != null)
            {
                if (cmd.Property(JKeys.Generic.ResponseRequired) != null)
                {
                    if (cmd.Property(JKeys.Generic.CommandID) == null)                    
                    {
                        NSULog.Debug(LogTag, "Command does not contain required 'CommandID' field.");
                        throw new Exception("Command does not contain required 'CommandID' field.");
                    }                    
                }
                NSULog.Debug(LogTag, "Adding command to queue.");
                queue.Enqueue(cmd);
                CheckCommand();
            }
            else
                NSULog.Debug(LogTag, "Command is NULL. Skipped.");
        }

        public void CheckCommand()
        {
            NSULog.Debug(LogTag, "CheckCommand()");
            if (current == null && queue.Count > 0 && string.IsNullOrEmpty(currentCmdID))
            {
                current = queue.Dequeue();
                if (current.Property(JKeys.Generic.CommandID) != null &&
                    !string.IsNullOrWhiteSpace((string)current[JKeys.Generic.CommandID]) &&
                    current.Property(JKeys.Generic.ResponseRequired) != null &&
                    Convert.ToBoolean((string)current[JKeys.Generic.ResponseRequired]))
                {
                    current.Remove(JKeys.Generic.ResponseRequired);
                    currentCmdID = (string)current[JKeys.Generic.CommandID];
                    timer.Start();
                }
                else
                {
                    currentCmdID = string.Empty;
                }
                RaiseCurrentAsync();
            }
        }

        public void ResponseReceived(JObject cmd)
        {
            NSULog.Debug(LogTag, "ResponseReceived()");
            if (cmd.Property(JKeys.Generic.CommandID) != null)
            {
                var cmdID = (string)cmd[JKeys.Generic.CommandID];
                if (!string.IsNullOrEmpty(currentCmdID) && currentCmdID.Equals(cmdID))
                {
                    NSULog.Debug(LogTag, "CmdID OK. Stopping communication timer.");
                    timer.Stop();
                    currentCmdID = string.Empty;
                }
            }

            try
            {
                CheckCommand();
            }
            catch (Exception e)
            {
                NSULog.Exception(LogTag, "RespondReceived(): " + e.Message);
            }
        }

        public void Pause()
        {
            //paused = true;
            if (current != null)
            {
                timer.Stop();
            }
        }

        public void Resume()
        {
            //paused = false;
            if (current != null)
            {
                RaiseCurrentAsync();
            }
            else
            {
                CheckCommand();
            }
        }

        public void Clear()
        {
            current = null;
            currentCmdID = string.Empty;
            timer.Stop();
            queue.Clear();
        }
    }
}

#endif