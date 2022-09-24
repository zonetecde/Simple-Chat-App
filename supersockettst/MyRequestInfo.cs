using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supersockettst
{ 
    public class MyRequestInfo : IRequestInfo
    {
        public MyRequestInfo(byte[] header, byte[] bodyBuffer)
        {
            Key = ASCIIEncoding.ASCII.GetString(new byte[] { header[0], header[1] });
            Data = bodyBuffer;
        }

        public string Key { get; set; }

        public byte[] Data { get; set; }

        public string Body
        {
            get
            {
                return Encoding.UTF8.GetString(Data);
            }
        }
    }
}
