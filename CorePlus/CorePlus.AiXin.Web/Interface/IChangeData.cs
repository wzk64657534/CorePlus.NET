using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorePlus.AiXin.Web
{
    public interface IChangeData
    {
        string Change(string data);
        string Return(string data, string type);
    }
}