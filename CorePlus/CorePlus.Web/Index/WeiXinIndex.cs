using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Repository;
using CorePlus.Entity;
using CorePlus.WeiXin.Repository;

namespace CorePlus.Web
{
    public class WeiXinIndex : BaseIndex
    {
        protected override List<Entity.SideMenuInfoEntity> GetSideMenu(string[] args)
        {
            string username = args[0];
            int menuId = GetMenuId();
            List<SideMenuInfoEntity> list = new List<SideMenuInfoEntity>();
            var user = new WxAccountRepository().FindByExpression(x => x.UserName == username).FirstOrDefault();
            SideMenuRepository repository = new SideMenuRepository();
            List<SideMenuInfoEntity> sideMenus;
            if (user == null)
            {
                sideMenus = repository.FindByExpression(x => x.MenuID == menuId && x.ID == 60)
                   .OrderBy(x => x.ID).ToList();
            }
            else
            {
                if (!user.IsAdvanced)
                {
                    sideMenus = repository.FindByExpression(x => x.MenuID == menuId && x.ParentMenuID == 0 && x.ID != 70)
                      .OrderBy(x => x.ID).ToList();
                }
                else
                {
                    sideMenus = repository.FindByExpression(x => x.MenuID == menuId && x.ParentMenuID == 0)
                       .OrderBy(x => x.ID).ToList();
                }
            }

            foreach (var item in sideMenus)
            {
                list.Add(item);
                var childMenu = repository.FindByExpression(x => x.ParentMenuID == item.ID).ToList();
                if (childMenu.Count > 0)
                {
                    list.AddRange(childMenu);
                }
            }

            return list;
        }

        protected override int GetMenuId()
        {
            return 12;
        }
    }
}