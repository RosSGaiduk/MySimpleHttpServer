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
    class Program
    {
        static void Main(string[] args)
        {
            int MaxThreadsCount = Environment.ProcessorCount * 4;

            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);

            ThreadPool.SetMinThreads(2, 2);

            new WebServer(80);
        }
    }
}
