using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Core
{
    public class XmlHelper
    {
        public static TEntity Deserialize<TEntity>(string xml)
            where TEntity : class
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TEntity));
                return (TEntity)serializer.Deserialize(sr);
            }
        }

        public static string Serialize<TEntity>(TEntity entity)
            where TEntity : class
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TEntity));
                serializer.Serialize(sw, entity);
                return sw.ToString();
            }
        }
    }
}