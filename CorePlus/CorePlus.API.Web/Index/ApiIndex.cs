using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorePlus.Entity;
using Core;
using System.Data.Objects.SqlClient;
using System.Web.Mvc;
using System.Text;

namespace CorePlus.API.Web
{
    public class ApiIndex : BaseIndex
    {
        protected override List<SideMenuInfoEntity> GetSideMenu(string[] args)
        {
            var db = CoreDBContext.GetContext();

            ParamWebHelper.GetParamDtsInfo(14);
            var parameters = ParamWebHelper.GetParamDtsInfo(14);

            List<SideMenuInfoEntity> list = new List<SideMenuInfoEntity>();
            foreach (var item in parameters)
            {
                list.Add(new SideMenuInfoEntity
                {
                    ID = long.Parse(item.ParamValue),
                    MenuName = item.ParamDtsName,
                    ParentMenuID = 0
                });

                string name = args[0];
                var query = from x in db.Set<AccountInfoEntity>()
                            where SqlFunctions.StringConvert((decimal)x.CategoryValue).Trim() == item.ParamValue
                            && (from y in db.Set<UserInfoEntity>()
                                where y.UserName == name
                                && y.ID == x.UserId
                                select y).Any()
                            select x;

                foreach (var account in query)
                {
                    list.Add(new SideMenuInfoEntity
                    {
                        ID = account.ID,
                        MenuName = account.AccountChnName,
                        ParentMenuID = int.Parse(item.ParamValue),
                        Controller = "Baidu",
                        Action = "Index",
                        Parameters = account.ID + "/" + account.AccountName
                    });
                }

            }

            return list;
        }

        protected override void BeforeGetMenuHtml(string[] args, StringBuilder sb)
        {
            sb.Append("<li><a class=\"recordable close\" href=\"#\">账户管理</a>");
            sb.Append("<ul class=\"nav\" style=\"display: none\">");
            sb.Append("<a href=\"/Account/List\" target=\"ifrmIndex\">账户信息维护</a>");
            sb.Append("</ul>");
            sb.Append("</li>");
            sb.Append("<li><a class=\"recordable close\" href=\"#\">现有账户</a>");
            sb.Append("<ul class=\"nav\" style=\"display: none\">");
        }

        protected override void AfterMenuHtml(string[] args, StringBuilder sb)
        {
            sb.Append("</ul>");
            sb.Append("</li>");
        }
    }
}