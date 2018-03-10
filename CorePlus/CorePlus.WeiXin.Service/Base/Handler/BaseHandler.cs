using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.WeiXin.Service
{
    public abstract class BaseHandler : IHttpHandler
    {
        public virtual void ProcessRequest(HttpContext context)
        {
            RequestInfomation(context);
        }

        protected abstract void RequestInfomation(HttpContext context);

        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}