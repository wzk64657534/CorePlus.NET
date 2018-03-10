using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace CorePlus.WeiXin.Service
{
    public interface IKey
    {
        string GetValueOfColumns(string xml, string[] cols);
        string HasColumn(string xml, string col);
    }
}