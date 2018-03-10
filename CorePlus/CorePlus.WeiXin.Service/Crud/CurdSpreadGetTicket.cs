using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdSpreadGetTicket : BasePostCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_SPREAD_GET_TICKET;
        }
    }
}