using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace supersockettst
{
    public class MyServer : AppServer
    {
        public MyServer() : base(new DefaultReceiveFilterFactory())
        {
            this.NewSessionConnected += MyServer_NewSessionConnected;
            this.SessionClosed += MyServer_SessionClosed;
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }

        void MyServer_NewSessionConnected(MySession session)
        {
            LogHelper.Log("new client connection, sessionid =" + session.Sessionid.Substring(session.Sessionid.Length - 6) ToUpper());
        }

        void MyServer_SessionClosed(MySession session, CloseReason value)
        {
            LogHelper.Log("client loses connection, sessionid =" + session.Sessionid.Substring(session.Sessionid.Length - 6) Toupper() + ", reason:" + value);
        }
    }
}
