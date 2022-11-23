using NSU.Shared.Compress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSU.Shared.NSUNet
{
    public enum NetDataType
    {
        Unknown,
        Binary,
        String,
        CompressedString,
        PartialInit,
        Partial,
        PartialDone
    };

    public class InternalArgs
    {
        public NetDataType DataType { get; }
        public object Data { get; }

        public InternalArgs(NetDataType dataType, object data)
        {
            DataType = dataType;
            Data = data;
        }
    }

    public class DataAvailableEventArgs : EventArgs
    {
        public byte[] Data { get; }
        public DataAvailableEventArgs(byte[] data)
        {
            Data = data;
        }
    }

    internal class Buffer
    {
        const int HeaderSize = sizeof(byte) + sizeof(int);
        
        public event EventHandler<DataAvailableEventArgs>? DataAvailable;
        byte[] buff = new byte[0];

        private bool ValidateDataType(int value)
        {
            return value > 0 && Enum.IsDefined(typeof(NetDataType), value);
        }

        public void AddData(byte[] b, int ofs, int cnt)
        {
            if(buff.Length > 0)
            {
                if (!ValidateDataType(buff[0]))
                {
                    //NSULog.Debug(LogTag, $"AddData(). Remainer buff not empty. Data type not valid [{buff[0]}]. Clearing buff.");
                    buff = new byte[0];
                    return;
                }
            }
            else if(!ValidateDataType(b[ofs]))
            {
                //NSULog.Debug(LogTag, $"AddData(). Remainer buff empty. New data type not valid [{b[ofs]}]. Clearing buff.");
                buff = new byte[0];
                return;
            }
            byte[] tmp = new byte[buff.Length + cnt];
            Array.Copy(buff, tmp, buff.Length);
            Array.Copy(b, ofs, tmp, buff.Length, cnt);
            buff = tmp;
            CheckDataAvailable();
        }

        void CheckDataAvailable()
        {
            while(true)
            {
                try
                {
                    if (buff.Length >= HeaderSize)
                    {
                        NetDataType dt = (NetDataType)buff[0];
                        var dataL = BitConverter.ToInt32(buff, 1) + HeaderSize;
                        //NSULog.Debug(LogTag, $"CheckDataAvailable() DataType: {dt}, DataLength: {dataL}, buff Length: {buff.Length}");
                        if (dataL <= buff.Length)
                        {
                            DataAvailable?.Invoke(this, new DataAvailableEventArgs(PopData(dataL)));
                        }
                        else
                            break;
                    }
                    else
                        break;
                }
                catch(Exception ex)
                {
                    //NSULog.Exception(LogTag, ex.Message);
                    //clear data
                    _ = new byte[0];
                }
            }
        }

        byte[] PopData(int dataL)
        {
            byte[] ret = new byte[dataL];
            Array.Copy(buff, ret, dataL);
            int remBytes = buff.Length - dataL;
            if(remBytes > 0)
            {
                byte[] tmp = new byte[remBytes];
                Array.Copy(buff, dataL, tmp, 0, remBytes);
                buff = tmp;
            }
            else
            {
                buff = new byte[0];
            }
            //NSULog.Debug(LogTag, $"PopData() buff Length: {buff.Length}");
            return ret;
        }

        public void Clear()
        {
            buff = new byte[0];
        }
    }

    public class InternalArgBuilder
    {
        readonly string LogTag = "NetServer.InternalDataReceivedArgs";
        const int HeaderSize = sizeof(byte) + sizeof(int);
        readonly Buffer buffer;
        byte[]? intBuff;
        int partDataPos;
        readonly Queue<InternalArgs> queue = new Queue<InternalArgs>();
        public bool DataAvailable => queue.Any();

        public InternalArgBuilder()
        {
            buffer = new Buffer();
            buffer.DataAvailable += Buffer_OnDataAvailable;
        }

        private void Buffer_OnDataAvailable(object? sender, DataAvailableEventArgs e)
        {
            int idx = 0;
            NetDataType dt = (NetDataType)e.Data[idx++];
            int dataLength = BitConverter.ToInt32(e.Data, idx);
            idx += sizeof(int);
            byte[]? buff;
            switch (dt)
            {
                case NetDataType.PartialInit:                    
                    dataLength = BitConverter.ToInt32(e.Data, idx);
                    partDataPos = 0;
                    intBuff = null;
                    if (dataLength < 10 * 1024 * 1024) //check MAX data length
                    {
                        intBuff = new byte[dataLength];
                    }
                    return;

                case NetDataType.Partial:
                    if (intBuff != null)
                    {
                        Array.Copy(e.Data, idx, intBuff, partDataPos, dataLength);
                        partDataPos += dataLength;
                    }
                    return;

                case NetDataType.PartialDone:
                    buff = intBuff;
                    intBuff = null;
                    idx = 0;
                    if (buff?.Length >= HeaderSize)
                    {
                        dt = (NetDataType)buff[idx++];
                        dataLength = BitConverter.ToInt32(buff!, idx);
                        idx += sizeof(int);
                        break;
                    }
                    else
                    {
                        return;
                    }
                case NetDataType.Binary:
                case NetDataType.String:
                case NetDataType.CompressedString:
                    buff = e.Data;
                    break;
                case NetDataType.Unknown:
                default:
                    //NSULog.Debug(LogTag, $"Unknown DataType. Value: {buff[0].ToString()};");
                    //NSULog.Debug(LogTag, $"Array values: {Helper.GetReadableNetworkData(buff, 0, 25)}...");
                    Clear();
                    return;
            }

            if (buff == null || dataLength == 0)
                return;

            string finalS;
            switch (dt)
            {
                case NetDataType.String:
                    finalS = Encoding.ASCII.GetString(buff!, idx, dataLength);
                    break;
                case NetDataType.CompressedString:
                    finalS = StringCompressor.Unzip(buff!, idx, dataLength);
                    break;
                    default:
                    
                        return;
            }
            queue.Enqueue(new InternalArgs(NetDataType.String, finalS ));
        }

        public void Clear()
        {
            queue.Clear();
            intBuff = null;
        }

        public void Process(byte[] buff, int offset, int cnt)
        {
            buffer.AddData(buff, offset, cnt);
        }

        public InternalArgs GetArgs()
        {
            return queue.Dequeue();
        }
    }

    internal static class Helper
    {
        public static string GetReadableNetworkData(byte[] buf, int start, int count)
        {
            var sb = new StringBuilder();
            for (int i = start; i < count; i++)
            {
                sb.Append('#');
                sb.Append(buf[i].ToString());
            }
            return sb.ToString();
        }
    }
}
