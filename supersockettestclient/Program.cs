using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace supersockettestclient
{
    internal class Program
    {
        public static Socket _socket;

        public static NetworkStream _socketStream;

        static void Main(string[] args)
        {
            EndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2021);
            _socket = new Socket(serverAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(serverAddress);
            _socketStream = new NetworkStream(_socket);

            //SocketAsyncEventArgs socketAsyncArgs = new SocketAsyncEventArgs();
            //byte[] buffer = new byte[10240];
            //socketAsyncArgs.SetBuffer(buffer, 0, buffer.Length);
            //socketAsyncArgs.Completed += ReciveAsync;
            //_socket.ReceiveAsync(socketAsyncArgs);

            Receive(_socket);

            Console.ReadKey();

            Task.Run(() =>
            {
                Random rnd = new Random();
                string cmd = "EC";
                String MSG = "test message 00" + rnd.Next(0, 100).ToString("00");
                Send(cmd, "hello");
            });
        }

        public static void Send(string cmd, string msg)
        {
            byte[] cmdBytes = Encoding.ASCII.GetBytes(cmd);
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
            byte[] lengthBytes = BitConverter.GetBytes((short)msgBytes.Length);

            _socketStream.Write(cmdBytes, 0, cmdBytes.Length);
            _socketStream.Write(lengthBytes, 0, lengthBytes.Length);
            _socketStream.Write(msgBytes, 0, msgBytes.Length);
            _socketStream.Flush();
            Console.WriteLine("send:" + msg);
        }

        private static void ReciveAsync(object obj, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred > 0)
            {
                string data = ASCIIEncoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
                Log("receive:" + data);
            }
        }

        private static void Receive(Socket socket)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[10240];
                        int receiveCount = _socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                        if (receiveCount > 0)
                        {
                            string data = ASCIIEncoding.UTF8.GetString(buffer, 0, receiveCount);
                            Log("receive:" + data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("receive error:" + ex.Message + "\r \n" + ex.StackTrace);
                }
            }, TaskCreationOptions.LongRunning);
        }

        #region Log
        /// 
        ///Output log
        /// 
        private static void Log(string log)
        {
            {
                
                Console.WriteLine((DateTime.Now.ToString("HH:mm:ss.fff") + " " + log + "\r\n\r\n"));
            }
        }
        #endregion
    }
}
