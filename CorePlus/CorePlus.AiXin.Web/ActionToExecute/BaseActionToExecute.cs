using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public abstract class BaseActionToExecute : IActionToExecute
    {
        public abstract string ActionToExecute(string AParamsData, string AHMACMD5);
        public abstract string ActionUserLogin(string AParamsData, string AHMACMD5);
    }
}