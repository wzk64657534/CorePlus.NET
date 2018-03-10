using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Core
{
    public static class SocketExtensions
    {
        public static bool IsNull(this Socket socket)
        {
            return socket == null;
        }

        public static bool IsNotNull(this Socket socket)
        {
            return socket != null;
        }
    }
}
