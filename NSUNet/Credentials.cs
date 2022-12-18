using System;

namespace NSU.Shared.NSUNet
{
    public class Credentials
    {
        public enum Field
        {
            Host,
            Port,
            Hash,
            DeviceID
        }

        public event EventHandler<CredentialsInfoChangedEventArgs> InfoChanged;
        public event EventHandler<CredentialsPasswordChangedEventArgs> PasswordChanged;
        
        public string Host { get => _host; set => SetHost(value); }
        public int Port { get => _port; set => SetPort(value); }
        public string UserName => _userName;
        public string Password => _password;
        public string DeviceID { get => _deviceID; set => SetDeviceId(value); }
        public string Hash { get => _hash; set => SetHash(value); }
        public bool IsCredentialsOk => !string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(_password);
        public bool IsHostPortOk => CheckIsHostPortOk();
        public bool IsAllCredentialsOk => IsCredentialsOk && IsHostPortOk;
        public bool UseHashForLogin => !string.IsNullOrEmpty(Hash) && !string.IsNullOrEmpty(DeviceID);


        private string _host = string.Empty;
        private int _port = -1;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _deviceID = string.Empty;
        private string _hash = string.Empty;

        public Credentials()
        {
        }

        public void SetUsernameAndPassword(string newUsername, string newPassword, bool save)
        {
            if (_userName != newUsername || _password != newPassword)
            {
                string oldUsername = _userName;
                string oldPassword = _password;
                _userName = newUsername;
                _password = newPassword;
                if(save)
                    RaisePasswordChanged(oldUsername, oldPassword, newUsername, newPassword);
            }
        }

        private void SetHost(string value)
        {
            if (value != _host)
            {
                _host = value;
                RaiseInfoChanged(Field.Host, _host);
            }
        }
        
        private void SetPort(int value)
        {
            if (value != _port)
            {
                _port = value;
                RaiseInfoChanged(Field.Port, _port.ToString());
            }
        }

        private void SetDeviceId(string value)
        {
            if (_deviceID != value)
            {
                _deviceID = value;
                RaiseInfoChanged(Field.DeviceID, _deviceID);
            }
        }
        
        private void SetHash(string value)
        {
            if (_hash != value)
            {
                _hash = value;
                RaiseInfoChanged(Field.Hash, _hash);
            }
        }

        private bool CheckIsHostPortOk()
        {
            try
            {
                return Uri.CheckHostName(_host) != UriHostNameType.Unknown;
                //return (_hostValidator?.Invoke(_host)).GetValueOrDefault();
            }
            catch { }
            return false;
        }

        private void RaiseInfoChanged(Field infoField, string newValue)
        {
            var evt = InfoChanged;
            evt?.Invoke(this, new CredentialsInfoChangedEventArgs(infoField, newValue));
        }

        private void RaisePasswordChanged(string oldUsername, string oldPassword, string newUsername, string newPassword)
        {
            var evt = PasswordChanged;
            evt?.Invoke(this, new CredentialsPasswordChangedEventArgs(oldUsername, oldPassword, newUsername, newPassword));

        }
    }
}
