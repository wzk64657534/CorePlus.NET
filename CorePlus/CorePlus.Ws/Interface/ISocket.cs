using System.Collections.Generic;
using System.Xml.Linq;
using Core;
using CorePlus.Entity;

namespace CorePlus.Ws
{
    public interface ISocket
    {
        void SendMessageToP2PServer(SocketP2PMessageEntity entity);
    }
}