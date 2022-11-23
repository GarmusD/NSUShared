using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.NSUNet
{
    public class CredentialsInfoChangedEventArgs : EventArgs
    {
        public Credentials.Field Field { get; }
        public string NewValue { get; } 
        public CredentialsInfoChangedEventArgs(Credentials.Field field, string newValue)
        {
            Field = field;
            NewValue = newValue;
        }
    }

    public class CredentialsPasswordChangedEventArgs : EventArgs
    {
        public string OldUsername { get; }
        public string NewUsername { get; }
        public string OldPassword { get; }
        public string NewPassword { get; }
        public CredentialsPasswordChangedEventArgs( string oldUsername, string oldPassword,  string newUsername,string newPassword)
        {
            OldUsername = oldUsername;
            NewUsername = newUsername;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }

    public class NetAttemptReconnectEventArgs : EventArgs
    {
        public int ReconnectCount;
        public int min;
        public int sec;
    }

    public class DataReceivedEventArgs : EventArgs
    {
        public Newtonsoft.Json.Linq.JObject Data { get; }
        public DataReceivedEventArgs(Newtonsoft.Json.Linq.JObject data)
        {
            Data = data;
        }
    }

    public class ClientLogintFailureEventArgs : EventArgs
    {
        public string ErrCode { get; }
        public ClientLogintFailureEventArgs(string errCode)
        {
            ErrCode = errCode;
        }
    }

    public class NSUSocketDataReceivedEventArgs : EventArgs
    {
        public byte[] Buffer { get; }
        public int Count { get; }
        public NSUSocketDataReceivedEventArgs(byte[] buffer, int count)
        {
            Buffer = buffer;
            Count = count;
        }
    }
}
