using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.P2P.Server
{
    public delegate void SendToClientCompleteHandler(IAsyncResult async);
    public delegate void OutPutSystemMessageHandler(string message);
}