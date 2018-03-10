using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdUserNoticeList : BaseGetCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_USER_NOTICE_LIST;
        }

        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "next_openid" };
        }
    }
}