using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class ParamManager
    {
        private static List<IParamProvider> providers;
        public static List<IParamProvider> Providers
        {
            get
            {

                if (providers == null)
                {
                    providers = SpringHelper.GetObjectList<IParamProvider>();
                    providers.Add(new DefaultParamProvider());
                }
                return providers;
            }
        }

        public static string GetParamValue(string param)
        {
            string value = null;
            foreach (var item in Providers)
            {
                value = item.GetParam(param);
                if (value != null)
                    return value;
            }
            return null;
        }
    }
}
