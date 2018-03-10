using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Core
{
    public static class ObjectExtensions
    {
        public static void LoadData(this object obj, object data)
        {
            if (data != null)
            {
                Type type = obj.GetType();
                Type dataType = data.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (var item in properties)
                {
                    var propertyInfo = dataType.GetProperty(item.Name);
                    if (propertyInfo != null && !item.IsDefined(typeof(NotLoadAttribute), false) && propertyInfo.CanWrite && propertyInfo != null && propertyInfo.PropertyType == item.PropertyType)
                        item.SetValue(obj, propertyInfo.GetValue(data, null), null);
                }
            }
        }

        public static void CopyData(this object obj, object data)
        {
            if (data != null)
            {
                Type type = obj.GetType();
                Type dataType = data.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (var item in properties)
                {
                    var propertyInfo = dataType.GetProperty(item.Name);
                    if (!item.IsDefined(typeof(NotCopyAttribute), false) && propertyInfo.CanWrite && propertyInfo != null && propertyInfo.PropertyType == item.PropertyType)
                        item.SetValue(obj, propertyInfo.GetValue(data, null), null);
                }
            }
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}