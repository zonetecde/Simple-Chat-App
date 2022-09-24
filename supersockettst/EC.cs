using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace supersockettst
{
    public class EC : CommandBase
    {
        public override void ExecuteCommand(MySession session, MyRequestInfo requestInfo)
        {
            LogHelper.Log("client" + session.SessionID.Substring(session.SessionID.Length - 6).ToUpper() + "message sent:" + requestinfo Body);
            byte[] bytes = ASCIIEncoding.UTF8.GetBytes("message received");
            session.Send(bytes, 0, bytes.Length);
        }
    }
}
