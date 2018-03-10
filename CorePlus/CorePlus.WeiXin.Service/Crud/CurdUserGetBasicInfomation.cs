using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public class CurdUserGetBasicInfomation : BaseGetCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_USER_GET_BASIC_INFOMATION;
        }

        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "openid" };
        }
    }
}