using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdUserGroupUpdateName : BasePostCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_USER_GROUP_UPDATE_NAME;
        }
    }
}