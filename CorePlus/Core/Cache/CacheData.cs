using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class CacheData
    {
        public static event CacheHandler GetData;

        private static object lockObj = new object();

        protected static T GetCache<T>()
            where T : class
        {
            Type type = TypeHelper.GetChildType(typeof(T));
            string tableName = AttributesHelper.GetTableName(type);
            string cacheName = AttributesHelper.GetCacheName(type);
            if (!string.IsNullOrEmpty(tableName))
            {
                var cache = CacheProvider.GetCache<T>(tableName);
                if (cache == null)
                {
                    lock (lockObj)
                    {
                        cache = CacheProvider.GetCache<T>(tableName);
                        if (cache == null && GetData != null)
                        {
                            var list = GetData.GetInvocationList();
                            for (int i = list.Length - 1; i >= 0; i--)
                            {
                                try
                                {
                                    cache = list[i].DynamicInvoke(tableName) as T;
                                }
                                catch
                                {

                                }
                                if (cache != null) break;
                            }
                            CacheProvider.AddCache(tableName, cache, cacheName ?? tableName);
                        }
                    }
                }
                return cache;
            }
            throw new WarningException("无法缓存未设置表名的实体");
        }
    }
}
