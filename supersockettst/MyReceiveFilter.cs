using SuperSocket.Facility.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supersockettst
{
    public class MyReceiveFilter : FixedHeaderReceiveFilter
    {
        public MyReceiveFilter() : base(4)
        {

        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            return BitConverter.ToInt16(new byte[] { header[offset + 2], header[offset + 3] }, 0);
        }

        protected override MyRequestInfo ResolveRequestInfo(ArraySegment header, byte[] bodyBuffer, int offset, int length)
        {
            byte[] body = bodyBuffer.Skip(offset).Take(length).ToArray();
            return new MyRequestInfo(header.Array, body);
        }
    }
}
