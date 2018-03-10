using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdServantSendMessage : BasePostCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_SERVANT_SEND_MESSAGE;
        }
    }
}