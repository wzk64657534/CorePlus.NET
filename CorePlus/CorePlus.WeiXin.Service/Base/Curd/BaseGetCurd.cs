using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using CorePlus.WeiXin.Entity;

namespace CorePlus.WeiXin.Service
{
    public abstract class BaseGetCurd : BaseCurd
    {
        protected override string GetMethod()
        {
            return "GET";
        }
    }
}