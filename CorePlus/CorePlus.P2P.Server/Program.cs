using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CorePlus.P2P.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly.Load("CorePlus.Entity");
            P2PServer.Run();
            Console.ReadLine();
        }
    }
}