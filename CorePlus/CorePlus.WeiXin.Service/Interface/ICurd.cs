using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public interface ICurd
    {
        string Operation(NameValueCollection parameters);
    }
}