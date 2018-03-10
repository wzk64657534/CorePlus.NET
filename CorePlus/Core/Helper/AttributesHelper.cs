using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public static class AttributesHelper
    {
        public static string GetTableName<TEntity>()
        {
            return GetTableName(typeof(TEntity));
        }

        public static string GetTableName(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(ModuleAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return (attributes[0] as ModuleAttribute).Name;
            }
            attributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (attributes == null || attributes.Length < 1)
            {
                throw new Exception("未定义Table标签");
            }
            return (attributes[0] as TableAttribute).Name;
        }

        public static string GetCacheName(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(CacheAttribute), false);
            if (attributes == null || attributes.Length < 1)
            {
                return null;
            }
            return (attributes[0] as CacheAttribute).Name;
        }

        public static string GetInfoName(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(InfoAttribute), true);
            if (attributes == null || attributes.Length < 1)
            {
                return null;
            }
            return (attributes[0] as InfoAttribute).Name;
        }
    }
}