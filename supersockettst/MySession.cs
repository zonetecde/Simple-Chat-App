using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supersockettst
{
    public class MySession : AppSession
    {
        public MySession()
        {

        }

        protected override void OnSessionStarted()
        {

        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        protected override void HandleUnknownRequest(MyRequestInfo requestInfo)
        {

        }

        protected override void HandleException(Exception e)
        {

        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
    }
}
