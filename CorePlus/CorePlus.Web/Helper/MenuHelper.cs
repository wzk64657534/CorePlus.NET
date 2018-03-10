using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;

namespace CorePlus.Web
{
    public class MenuHelper
    {
        public static IQueryable<MenuInfoEntity> GetMainMenu(string name)
        {
            var db = CoreDBContext.GetContext();
            var query = from x in db.Set<MenuInfoEntity>()
                        join y in db.Set<RoleMenuInfoEntity>()
                        on x.ID equals y.MenuId
                        where (from z in db.Set<UserInfoEntity>()
                               where y.RoleId == z.RoleId && z.UserName == name
                               select z).Any()
                        && x.ID != 6
                        select x;
            return query;
        }
    }
}