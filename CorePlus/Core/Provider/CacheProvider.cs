using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Core
{
    public class CacheProvider
    {
        public static void AddCache(string key, object value, string tableName)
        {
            var database = Convert.ToString(HttpContext.Current.Session["Database"]);
            var dbContext = !string.IsNullOrEmpty(database) ? database : "CoreDBContext";

            var tableNames = tableName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            CacheDependency dependency = null;
            if (tableNames.Length > 1)
            {
                var aggDependency = new AggregateCacheDependency();
                foreach (var tName in tableNames)
                {
                    aggDependency.Add(new SqlCacheDependency(dbContext, tName));
                }
                dependency = aggDependency;
            }
            else
            {
                dependency = new SqlCacheDependency(dbContext, tableName);
            }
            HttpRuntime.Cache.Insert(key, value, dependency, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
        }

        public static T GetCache<T>(string key)
            where T : class
        {
            return HttpRuntime.Cache[key] as T;
        }
    }
}
