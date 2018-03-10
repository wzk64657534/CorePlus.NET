using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class BaseCache : CacheData
    {
        static BaseCache()
        {
            CacheData.GetData += new CacheHandler(CacheData_GetData);
        }

        static object CacheData_GetData(string tableName)
        {
            switch (tableName)
            {
                case "SysCodeRule":
                    var context = CoreDBContext.GetContext();
                    return context.Set<CodeRuleEntity>().Where(x => x.IsValid == true).ToList();
            }
            return null;
        }

        public static List<CodeRuleEntity> CodeRule
        {
            get { return GetCache<List<CodeRuleEntity>>(); }
        }
    }
}
