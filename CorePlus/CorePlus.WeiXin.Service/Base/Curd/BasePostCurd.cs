using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using CorePlus.WeiXin.Entity;

namespace CorePlus.WeiXin.Service
{
    public abstract class BasePostCurd : BaseCurd
    {
        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "body" };
        }

        protected override string GetMethod()
        {
            return "POST";
        }
    }
}