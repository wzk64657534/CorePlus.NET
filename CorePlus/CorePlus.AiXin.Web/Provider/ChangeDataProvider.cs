using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public class ChangeDataProvider
    {
        IChangeData changeData = null;

        public ChangeDataProvider(string key)
        {
            switch (key)
            {
                case "json": changeData = new JsonChangeData(); break;
                case "xml": changeData = new XmlChangeData(); break;
            }
        }

        public string Change(string data)
        {
            if (changeData == null) { return string.Empty; }
            return changeData.Change(data);
        }

        public string Return(string data, string type = "json")
        {
            if (changeData == null) { return string.Empty; }
            return changeData.Return(data, type);
        }
    }
}