using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace CorePlus.WeiXin.Service
{
    public class ManagerCurd
    {
        Dictionary<string, ICurd> manager = null;

        public ManagerCurd()
        {
            manager = new Dictionary<string, ICurd>();
            manager.Add("C001", new CurdMenuCreate());
            manager.Add("C002", new CurdMenuQuery());
            manager.Add("C003", new CurdMenuDelete());

            manager.Add("C004", new CurdUserGroupCreate());
            manager.Add("C005", new CurdUserGroupQueryAll());
            manager.Add("C006", new CurdUserGroupQueryUserId());
            manager.Add("C007", new CurdUserGroupUpdateName());
            manager.Add("C008", new CurdUserGroupMove());

            manager.Add("C009", new CurdSpreadGetTicket());
            manager.Add("C010", new CurdSpreadGetCode());

            manager.Add("C011", new CurdServantSendMessage());

            manager.Add("C012", new CurdUserNoticeList());

            manager.Add("C013", new CurdFileUploadMedia());
            manager.Add("C014", new CurdFileDownloadMedia());

            manager.Add("C015", new CurdPageOAuthFirst());
            manager.Add("C016", new CurdPageOAuthSecond());
            manager.Add("C017", new CurdPageOAuthThird());
            manager.Add("C018", new CurdPageOAuthFourth());
        }

        public static ManagerCurd CreateInstance()
        {
            return new ManagerCurd();
        }

        public string Operation(string key, NameValueCollection parameters)
        {
            key = key.ToUpper();
            if (manager.ContainsKey(key))
            {
                return manager[key].Operation(parameters);
            }
            else
            {
                return ConstHelper.ERROR_100002;
            }
        }
    }
}