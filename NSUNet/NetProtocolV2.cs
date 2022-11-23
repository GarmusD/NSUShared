using NSU.Shared.Compress;
using System;
using System.Collections.Generic;
using System.IO;

namespace NSU.Shared.NSUNet
{
    public class NetProtocolV2 : INetProtocol
    {
        private const int MAX_SEND_MESSAGE_SIZE = 512;
        private const int SEND_BUFFER_HEADER_SIZE = sizeof(byte) + sizeof(int);
        private const int MAX_SEND_CONTENT_SIZE = MAX_SEND_MESSAGE_SIZE - SEND_BUFFER_HEADER_SIZE;

        public INetMessage? Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        public List<byte[]> Encode(string data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var compressed = StringCompressor.Zip(data);
                ms.WriteByte((byte)NetDataType.CompressedString);
                ms.Write(BitConverter.GetBytes(compressed.Length), 0, sizeof(int));
                ms.Write(compressed, 0, compressed.Length);
                return Multipart(ms.ToArray());
            }
        }

        public List<byte[]> Encode(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.WriteByte((byte)NetDataType.Binary);
                ms.Write(BitConverter.GetBytes(data.Length), 0, sizeof(int));
                ms.Write(data, 0, data.Length);
                ms.Flush();
                return Multipart(ms.ToArray());
            }
        }

        public List<byte[]> Multipart(byte[] container)
        {
            if (container.Length <= MAX_SEND_MESSAGE_SIZE)
            {
                return new List<byte[]> { container };
            }

            List<byte[]> multipart = new List<byte[]>()
            {
                CreatePartialStart(container.Length)
            };

            int partStartPos = 0;
            while (true)
            {
                multipart.Add(CreatePartialContent(container, partStartPos));
                partStartPos += MAX_SEND_CONTENT_SIZE;
                if (partStartPos >= container.Length)
                    break;
            }

            multipart.Add(CreatePartialDone());
            return multipart;
        }

        private byte[] CreatePartialStart(int totalLength)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.WriteByte((byte)NetDataType.PartialInit);
                ms.Write(BitConverter.GetBytes(sizeof(int)), 0, sizeof(int));
                ms.Write(BitConverter.GetBytes(totalLength), 0, sizeof(int));
                ms.Flush();
                return ms.ToArray();
            }
        }

        private byte[] CreatePartialContent(byte[] content, int start)
        {
            if (start < content.Length)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    int partLength = Math.Min(content.Length - start, MAX_SEND_CONTENT_SIZE);
                    //_logger.Debug($"Bytes left in part: {leftL}");
                    ms.WriteByte((byte)NetDataType.Partial);
                    ms.Write(BitConverter.GetBytes(partLength), 0, sizeof(int));
                    ms.Write(content, start, partLength);
                    ms.Flush();
                    return ms.ToArray();
                }
            }
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        private byte[] CreatePartialDone()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.WriteByte((byte)NetDataType.PartialDone);
                ms.Write(BitConverter.GetBytes(sizeof(int)), 0, sizeof(int));
                ms.Write(BitConverter.GetBytes((int)1), 0, sizeof(int));
                ms.Flush();
                return ms.ToArray();
            }
        }
    }
}
