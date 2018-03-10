using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;

namespace CorePlus.P2P.Server
{
    public interface IIdentity
    {
        void Comunicate(SocketP2PEntity client, SocketP2PMessageEntity message, List<SocketP2PEntity> clients, List<SocketP2PEntity> queue,
           SendToClientCompleteHandler sendToClientCompleteHandler, OutPutSystemMessageHandler outPutSystemMessageHandler);
    }
}