using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using System.Web.Mvc;
using System.Text;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public abstract class BaseIndex : IIndex
    {
        protected virtual List<SideMenuInfoEntity> GetSideMenu(string[] args)
        {
            int menuId = GetMenuId();
            SideMenuRepository repository = new SideMenuRepository();
            List<SideMenuInfoEntity> list = new List<SideMenuInfoEntity>();
            var sideMenus = repository.FindByExpression(x => x.MenuID == menuId && x.ParentMenuID == 0).OrderBy(x => x.ID).ToList();
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

        protected virtual int GetMenuId()
        {
            return 0;
        }

        public string GetMenuHtml(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            BeforeGetMenuHtml(args, sb);

            var list = GetSideMenu(args);
            var parentMenus = list.Where(x => x.ParentMenuID == 0);
            foreach (var parent in parentMenus)
            {
                sb.Append("<li>");
                sb.AppendFormat("<a class=\"recordable close\" href=\"#\">{0}</a>", parent.MenuName);
                var childMenus = list.Where(x => x.ParentMenuID == parent.ID);
                if (childMenus.Any())
                {
                    sb.Append("<ul style=\"display: none\">");
                    foreach (var child in childMenus)
                    {
                        sb.Append("<li>");
                        sb.AppendFormat("<a href=\"/{1}/{2}/{3}\" target=\"ifrmIndex\">{0}</a>",
                            child.MenuName, child.Controller, child.Action, child.Parameters);
                        sb.Append("</li>");
                    }
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }

            AfterMenuHtml(args, sb);
            return sb.ToString();
        }

        protected virtual void BeforeGetMenuHtml(string[] args, StringBuilder sb)
        {

        }

        protected virtual void AfterMenuHtml(string[] args, StringBuilder sb)
        {

        }
    }
}