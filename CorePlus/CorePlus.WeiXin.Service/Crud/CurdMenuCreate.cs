using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdMenuCreate : BasePostCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_MENU_CREATE;
        }
    }
}