using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorePlus.Entity;
using Core;

namespace CorePlus.P2P.Server
{
    public class ManagerIdentity
    {
        public static void Execute(SocketP2PEntity client, SocketP2PMessageEntity message,
            List<SocketP2PEntity> Clients, List<SocketP2PEntity> Queue,
            SendToClientCompleteHandler SendToClientCompleteHandler, OutPutSystemMessageHandler OutPutSystemMessageHandler)
        {
            IIdentity id = null;

            switch (message.Identity.ToUpper())
            {
                case "CUSTOMER":
                    id = new CustomerIdentity();
                    break;
                case "SERVANT":
                    id = new ServantIdentity();
                    break;
            }

            id.Comunicate(client, message, Clients, Queue, SendToClientCompleteHandler, OutPutSystemMessageHandler);
        }
    }
}