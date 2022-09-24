using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace superSocketClient
{
    class Program
    {
        static Socket socketClient { get; set; }
        static void Main(string[] args)
        {
            //Create an instance
            socketClient = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint point = new IPEndPoint(ip, 30776);
            //Make connection
            socketClient.Connect(point); // THIS LINE

            //Receive messages from the server continuously
            Thread thread = new Thread(Recive);
            thread.IsBackground = true;
            thread.Start();

            //Send data to the server continuously
            //Thread thread2 = new Thread(Send);
            //thread2.IsBackground = true;
            //thread2.Start();

            Console.ReadKey();

            while (true)
            {
                Send(Console.ReadLine());
            }

        }

        static string connexionId;

        ///<summary>
        ///receive message
        ///</summary>
        ///<param name="o"></param>
        static void Recive()
        {
            //Why is it possible to use a telnet client, but not this one.
            while (true)
            {
                //Get the message sent
                byte[] buffer = new byte[1024 * 1024 * 2];
                var effective = socketClient.Receive(buffer);


                var str = Encoding.UTF8.GetString(buffer, 0, effective);

                if (!str.Contains("[server-connexion] as "))
                    Console.WriteLine(str);
                else
                    connexionId = str.Substring(str.IndexOf("[server-connexion] as ") + "[server-connexion] as ".Length).Replace("\r\n", string.Empty);
            }
        }

        static void Send(string text)
        {

            var buffter = Encoding.UTF8.GetBytes("\t" + connexionId  + " > " +  text + "\r\n");


            var temp = socketClient.Send(buffter);          
        }
    }
}
