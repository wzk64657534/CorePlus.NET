using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace CorePlus.AiXin.Web
{
    public class JsonChangeData : BaseChangeData
    {
        public override string Change(string data)
        {
            string json = data; //base.Change(data);
            json = json.Trim(new char[] { '{', '}' });
            string[] ary = json.Split(new char[] { ',' }, StringSplitOptions.None);

            StringBuilder sb = new StringBuilder();
            sb.Append("<Root>");
            foreach (var item in ary)
            {
                string[] children = item.Split(new char[] { ':' }, StringSplitOptions.None);
                string k = children[0].Trim(new char[] { '"' });
                string v = children[1].Trim(new char[] { '"' }).Replace("|", ",");
                sb.AppendFormat("<{0}>{1}</{0}>", k, v);
            }
            sb.Append("</Root>");
            return sb.ToString();
        }
    }
}