using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace MyHttpServer
{
    class WebServer
    {
        TcpListener Listener;


        public WebServer(int Port)
        {
            Listener = new TcpListener(IPAddress.Any, Port);
            Listener.Start();
            Console.Write("sadasdasd"); //виконається 1 раз при створенні
            while (true)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), Listener.AcceptTcpClient());
                Console.WriteLine("laaaaaaaa"); //буде виводитись при переході по ссилці
                //при натисканні кнопки надіслати, якраз виконується виведення повідомлення
            }
        }

        static void ClientThread(Object StateInfo)
        {
            new Client((TcpClient)StateInfo); //без цього не переходить між ссилками
        }

        static void catchRequest()
        {
            //string n = String.Format("{0}", Request.Form["customerName"]);        
        }



        ~WebServer()
        {
            if (Listener != null)
            {
                Listener.Stop();
            }
        }
    }
}
