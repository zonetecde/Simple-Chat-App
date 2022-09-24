using SuperSocket.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace supersockettst
{
    internal class Program
    {

        static void Main(string[] args)
        {
            MyServer _myServer;

            LogHelper.Init(this, txtLog);

            _myServer = new MyServer();
            ServerConfig serverConfig = new ServerConfig()
            {
                Port = 2021
            };
            _myServer.Setup(serverConfig);
            _myServer.Start();

            Console.ReadKey();

            foreach (MySession session in _myServer.GetAllSessions())
            {
                byte[] bytes = ASCIIEncoding.UTF8.GetBytes("server broadcast message");
                session.Send(bytes, 0, bytes.Length);
            }
        }
    }
}
