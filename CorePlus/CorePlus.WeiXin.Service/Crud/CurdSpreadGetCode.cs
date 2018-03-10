using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class CurdSpreadGetCode : BaseGetCurd
    {
        protected override string GetUrl()
        {
            return ConstHelper.CURD_SPREAD_GET_CODE;
        }

        protected override string[] GetNamesOfMustParamter()
        {
            return new string[] { "ticket" };
        }
    }
}