using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public class WebActionToExecute : BaseActionToExecute
    {
        public override string ActionToExecute(string AParamsData, string AHMACMD5)
        {
            AiXinMobileServerService.AixinMobileServerClient client = new AiXinMobileServerService.AixinMobileServerClient();
            return client.WebActionToExecute(AParamsData, AHMACMD5);
        }

        public override string ActionUserLogin(string AParamsData, string AHMACMD5)
        {
            AiXinMobileServerService.AixinMobileServerClient client = new AiXinMobileServerService.AixinMobileServerClient();
            return client.WebActionUserLogin(AParamsData, AHMACMD5);
        }
    }
}