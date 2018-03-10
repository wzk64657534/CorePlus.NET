using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdUserGroupQueryUserId : BasePostCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_USER_GROUP_QUERY_USERID;
        }
    }
}