using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdPageOAuthFourth : BaseGetCurd
    {
        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "web_token", "openid" };
        }

        protected override string GetUrl()
        {
            return ConstHelper.CURD_PAGE_OAUTH_4;
        }

        protected override void UpdateParameters(NameValueCollection parameters)
        {
            parameters["access_token"] = parameters["web_token"];
        }
    }
}