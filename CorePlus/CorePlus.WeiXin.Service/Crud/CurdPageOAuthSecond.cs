using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdPageOAuthSecond : BaseGetCurd
    {
        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "code" };
        }

        protected override string GetUrl()
        {
            return ConstHelper.CURD_PAGE_OAUTH_2;
        }

        protected override void UpdateParameters(NameValueCollection parameters)
        {
            parameters.Add("appid", ConfigurationManager.AppSettings["appid"].ToString());
            parameters.Add("secret", ConfigurationManager.AppSettings["secret"].ToString());
        }
    }
}