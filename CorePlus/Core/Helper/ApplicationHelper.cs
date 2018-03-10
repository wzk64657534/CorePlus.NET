using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;

namespace Core
{
    public class ApplicationHelper
    {
        public static object GetValue(string key)
        {
            if (HttpContext.Current.Application[key] != null)
            {
                return HttpContext.Current.Application[key];
            }

            return null;
        }

        public static void SetValue(string key, object value)
        {
            HttpContext.Current.Application[key] = value;
        }

        public static bool Exists(string key)
        {
            return HttpContext.Current.Application[key] != null;
        }

        public static List<TEntity> GetEntitys<TEntity>(string key)
            where TEntity : class
        {
            return (List<TEntity>)GetValue(key);
        }

        public static TEntity GetEntity<TEntity>(string key)
            where TEntity : class
        {
            return (TEntity)GetValue(key);
        }

        public static List<TEntity> GetCacheOfEntity<TEntity>()
            where TEntity : class
        {
            string tableName = AttributesHelper.GetTableName<TEntity>();
            if (!Exists(tableName))
            {
                SetCache<TEntity>();
            }

            return GetEntitys<TEntity>(tableName);
        }

        public static void SetCache<TEntity>()
            where TEntity : class
        {
            var db = CoreDBContext.GetContext();
            string tableName = AttributesHelper.GetTableName<TEntity>();
            SetValue(tableName, db.Set<TEntity>().ToList());
        }
    }
}