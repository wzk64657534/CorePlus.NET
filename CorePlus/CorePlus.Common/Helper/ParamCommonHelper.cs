using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CorePlus.Entity;

namespace CorePlus.Common
{
    public class ParamCommonHelper
    {
        public static string[] Hours = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };

        public static string GetValueByDtsName(long paramId, string dtsName)
        {

            List<ParamDtsInfoEntity> paramDts = ApplicationCommonHelper.GetCacheOfEntity<ParamDtsInfoEntity>();

            return (from x in paramDts
                    where x.ID == paramId
                    && x.ParamDtsName == dtsName
                    select x.ParamValue).FirstOrDefault();
        }
    }
}