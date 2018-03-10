using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CorePlus.Entity;
using CorePlus.Common;

namespace CorePlus.API.Web
{
    public class ParamWebHelper : ParamCommonHelper
    {
        public static string GetDiscriptionById(long paramId, string value)
        {
            List<ParamDtsInfoEntity> paramDts = ApplicationWebHelper.GetParamDtsInfo();

            return (from x in paramDts
                    where x.ID == paramId
                    && x.ParamValue == value
                    select x.ParamDtsName).FirstOrDefault();
        }

        public static List<ParamDtsInfoEntity> GetParamDtsInfo(long paramId)
        {
            List<ParamDtsInfoEntity> paramDts = ApplicationWebHelper.GetParamDtsInfo();
            return (from x in paramDts
                    where x.ID == paramId
                    select x).ToList();
        }

        internal static string GetValueByDtsName(long paramId, string dtsName)
        {
            List<ParamDtsInfoEntity> paramDts = ApplicationWebHelper.GetParamDtsInfo();

            return (from x in paramDts
                    where x.ID == paramId
                    && x.ParamDtsName == dtsName
                    select x.ParamValue).FirstOrDefault();
        }
    }
}