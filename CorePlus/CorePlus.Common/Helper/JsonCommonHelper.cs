using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;

using Core;
using CorePlus.Entity;

namespace CorePlus.Common
{
    public class JsonCommonHelper
    {
        public static string GetJson<TEntity>(List<TEntity> entitys)
            where TEntity : class
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(entitys);
            json = "{total:" + entitys.Count + ",rows:" + json + "}";
            return json;
        }
    }
}