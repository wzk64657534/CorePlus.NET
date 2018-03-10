using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorePlus.Web
{
    public class IndexManager
    {
        Dictionary<int, IIndex> manager = null;

        public IndexManager()
        {
            manager = new Dictionary<int, IIndex>();
            manager.Add(6, new ApiIndex());
            manager.Add(7, new VisitIndex());
            manager.Add(11, new ServantIndex());
            manager.Add(12, new WeiXinIndex());
        }

        public string GetLeftMenu(int key, string[] args)
        {
            if (manager.ContainsKey(key))
            {
                return manager[key].GetMenuHtml(args);
            }

            return null;
        }
    }
}