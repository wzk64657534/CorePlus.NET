using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdPageOAuthThird : BaseGetCurd
    {
        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "refresh_token" };
        }

        protected override string GetUrl()
        {
            return ConstHelper.CURD_PAGE_OAUTH_3;
        }

        protected override void UpdateParameters(NameValueCollection parameters)
        {
            parameters.Add("appid", ConfigurationManager.AppSettings["appid"].ToString());
        }
    }
}