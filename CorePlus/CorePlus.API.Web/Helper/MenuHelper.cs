using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;

namespace CorePlus.API.Web
{
    public class MenuHelper
    {
        public static IQueryable<MenuInfoEntity> GetMainMenu(string name)
        {
            var db = CoreDBContext.GetContext();
            var query = from x in db.Set<MenuInfoEntity>()
                        where x.ID == 6
                        select x;
            return query;
        }
    }
}