using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CorePlus.WeiXin.Entity
{
    [Serializable]
    public class ErrorEntity
    {
        [XmlElement("errcode")]
        public string errcode { get; set; }

        [XmlElement("errmsg")]
        public string errmsg { get; set; }
    }
}