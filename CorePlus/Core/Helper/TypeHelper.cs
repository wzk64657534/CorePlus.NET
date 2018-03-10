using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Core
{
    public class TypeHelper
    {
        public static Type GetChildType(Type type)
        {
            if (type.IsGenericType)
            {
                Type[] genericArgTypes = type.GetGenericArguments();
                return genericArgTypes[0];
            }
            return type;
        }

        public static T GetPropertyAttribute<T>(PropertyInfo propertyInfo)
            where T : Attribute
        {
            if (propertyInfo != null)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(T), true);
                if (attributes.Length > 0)
                {
                    return attributes[0] as T;
                }
            }
            return null;
        }

        public static object GetPropertyValue(object entity, string fieldName)
        {
            Type type = entity.GetType();
            var property = type.GetProperty(fieldName);
            if (property == null)
            {
                throw new WarningException("未找到字段" + fieldName);
            }
            return property.GetValue(entity, null);
        }

        public static object GetPropertyValue(object[] entitys, string tableName, string fieldName)
        {
            object entity = null;
            foreach (var item in entitys)
            {
                if (item != null && AttributesHelper.GetTableName(item.GetType()) == tableName)
                {
                    entity = item;
                    break;
                }
            }
            if (entity == null)
            {
                throw new Exception(string.Format("未能从参数中解析到表名为{0}的数据!", tableName));
            }
            return GetPropertyValue(entity, fieldName);
        }

        public static object GetRepository(string controller)
        {
            var namespaces = ConfigurationHelper.Namespaces;
            if (!string.IsNullOrEmpty(namespaces))
            {
                try
                {
                    var ns = StringHelper.Split(namespaces, ',');
                    foreach (var item in ns)
                    {
                        var type = Type.GetType(string.Format("{0}.{1}Repository,{0}", item, controller));
                        if (type != null)
                        {
                            return Activator.CreateInstance(type);
                        }
                    }
                }
                catch
                {
                    //LogHelper.LogError("", ex);
                }
            }
            return null;
        }
    }
}