using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using Core;
using CorePlus.WeiXin.Entity;
using CorePlus.WeiXin.Repository;
using System.Collections.Specialized;
using CorePlus.Common;

namespace CorePlus.WeiXin.Service
{
    public class curd : BaseHandler
    {
        protected override void RequestInfomation(HttpContext context)
        {
            string data = string.Empty;
            if (context.Request.Params["key"] == null)
            {
                data = ConstHelper.ERROR_100001;
            }
            else if (context.Request.Params["un"] == null)
            {
                data = ConstHelper.ERROR_100005;
            }
            else if (context.Request.Params["unkey"] == null)
            {
                data = ConstHelper.ERROR_100006;
            }
            else
            {
                string key = context.Request.Params.Get("key");
                NameValueCollection nvList = new NameValueCollection();
                nvList.Add(context.Request.QueryString);
                nvList.Add(context.Request.Form);

                data = CurdProvider.Operation(key, nvList);
                nvList = null;
            }

            context.Response.Write(data);
            context.Response.End();
        }
    }
}