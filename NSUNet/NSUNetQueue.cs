#if !NSUWATCHER
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Serilog;

namespace NSU.Shared.NSUNet
{
    public class NSUNetQueue
    {
        public delegate void CommandAvailableHandler(JObject cmd);
        public delegate void ResponseReceiveTimeoutHandler();

        public event CommandAvailableHandler OnCommandAvailable;
        public event ResponseReceiveTimeoutHandler OnResponseTimeout;

        private readonly ILogger _logger;
        readonly Queue<JObject> _queue;
        JObject _current;
        string _currentCmdID;
        //bool paused;
        NSUTimer _timer;
        readonly object lck = new object();


        public NSUNetQueue()
        {
            _logger = Log.Logger.ForContext<NSUNetQueue>(true);
            _queue = new Queue<JObject>();
            _timer = new NSUTimer(15000);
            _timer.OnNSUTimer += OnCommandResponseTimerHandler;
            _currentCmdID = string.Empty;
        }

        void OnCommandResponseTimerHandler()
        {
            _logger.Debug($"OnCommandResponseTimerHandler().  currentCmdID: '{_currentCmdID}'.");
            _timer.Stop();
            OnResponseTimeout?.Invoke();
        }

        private void RaiseCurrentAsync()
        {
            var c = _current;
            _current = null;
            _logger.Debug("RaiseCurrent() in async mode.");
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
            _logger.Debug("Add(cmd)");
            if (cmd != null)
            {
                if (cmd.Property(JKeys.Generic.ResponseRequired) != null)
                {
                    if (cmd.Property(JKeys.Generic.CommandID) == null)                    
                    {
                        _logger.Debug("Command does not contain required 'CommandID' field.");
                        throw new Exception("Command does not contain required 'CommandID' field.");
                    }                    
                }
                _logger.Debug("Adding command to queue.");
                _queue.Enqueue(cmd);
                CheckCommand();
            }
            else
                _logger.Debug("Command is NULL. Skipped.");
        }

        public void CheckCommand()
        {
            _logger.Debug("CheckCommand()");
            if (_current == null && _queue.Count > 0 && string.IsNullOrEmpty(_currentCmdID))
            {
                _current = _queue.Dequeue();
                if (_current.Property(JKeys.Generic.CommandID) != null &&
                    !string.IsNullOrWhiteSpace((string)_current[JKeys.Generic.CommandID]) &&
                    _current.Property(JKeys.Generic.ResponseRequired) != null &&
                    Convert.ToBoolean((string)_current[JKeys.Generic.ResponseRequired]))
                {
                    _current.Remove(JKeys.Generic.ResponseRequired);
                    _currentCmdID = (string)_current[JKeys.Generic.CommandID];
                    _timer.Start();
                }
                else
                {
                    _currentCmdID = string.Empty;
                }
                RaiseCurrentAsync();
            }
        }

        public void ResponseReceived(JObject cmd)
        {
            _logger.Debug("ResponseReceived()");
            if (cmd.Property(JKeys.Generic.CommandID) != null)
            {
                var cmdID = (string)cmd[JKeys.Generic.CommandID];
                if (!string.IsNullOrEmpty(_currentCmdID) && _currentCmdID.Equals(cmdID))
                {
                    _logger.Debug("CmdID OK. Stopping communication timer.");
                    _timer.Stop();
                    _currentCmdID = string.Empty;
                }
            }

            try
            {
                CheckCommand();
            }
            catch (Exception e)
            {
                _logger.Error(e, "RespondReceived(): Exception: {ex}");
            }
        }

        public void Pause()
        {
            //paused = true;
            if (_current != null)
            {
                _timer.Stop();
            }
        }

        public void Resume()
        {
            //paused = false;
            if (_current != null)
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
            _current = null;
            _currentCmdID = string.Empty;
            _timer.Stop();
            _queue.Clear();
        }
    }
}

#endif