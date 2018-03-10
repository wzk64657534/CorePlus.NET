using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Core
{
    public class JsonHelper
    {
        public static TEntity Deserialize<TEntity>(string json)
            where TEntity : class
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<TEntity>(json);
        }

        public static string Serialize(object entity)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(entity);
        }
    }
}