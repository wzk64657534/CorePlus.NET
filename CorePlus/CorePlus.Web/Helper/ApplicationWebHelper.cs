using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CorePlus.Entity;
using Core;
using CorePlus.Common;


namespace CorePlus.Web
{
    public class ApplicationWebHelper : ApplicationCommonHelper
    {
        public static List<ParamDtsInfoEntity> GetParamDtsInfo()
        {
            return GetCacheOfEntity<ParamDtsInfoEntity>();
        }
    }
}