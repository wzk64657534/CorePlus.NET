using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context.Support;
using Spring.Context;

namespace Core
{
    class SpringHelper
    {
        public static T GetObject<T>(bool warning = false)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            var dict = ctx.GetObjectsOfType(typeof(T));
            if (warning && dict.Keys.Count == 0)
            {
                throw new WarningException("未配置类型的{0}的相关信息", typeof(T).Name);
            }
            foreach (var item in dict.Values)
            {
                return (T)item;
            }
            return default(T);
        }

        public static List<T> GetObjectList<T>(bool warning = false)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            var dict = ctx.GetObjectsOfType(typeof(T));
            if (warning && dict.Keys.Count == 0)
            {
                throw new WarningException("未配置类型的{0}的相关信息", typeof(T).Name);
            }
            List<T> list = new List<T>();
            foreach (var item in dict.Values)
            {
                list.Add((T)item);
            }
            return list;
        }

        public static T GetObject<T>(string name, bool warning = false)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            var obj = ctx.GetObject(name);
            if (warning && obj == null)
            {
                throw new WarningException("未配置名称为{0}的相关信息", name);
            }
            return (T)obj;
        }
    }
}
