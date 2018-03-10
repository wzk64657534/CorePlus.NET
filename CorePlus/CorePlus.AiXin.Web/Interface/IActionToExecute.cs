using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public interface IActionToExecute
    {
        string ActionToExecute(string AParamsData, string AHMACMD5);
        string ActionUserLogin(string AParamsData, string AHMACMD5);
    }
}