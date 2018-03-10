using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorePlus.API.Web
{
    public class IndexManager
    {
        Dictionary<int, IIndex> manager = null;

        public IndexManager()
        {
            manager = new Dictionary<int, IIndex>();
            manager.Add(6, new ApiIndex());
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