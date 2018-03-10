using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Core;
using CorePlus.WeiXin.Entity;
using System.Data.Objects.SqlClient;
using CorePlus.Common;
using CorePlus.Entity;

namespace CorePlus.WeiXin.Repository
{
    public class WxMenuRepository : WxUserNameRepository<WxMenuEntity>
    {
        public List<SelectListItem> GetInFirstLevel(string username)
        {
            var query = (from x in this.DbSet
                         where (x.HighId == 0
                         || x.HighId == null)
                         && x.UserName == username
                         select new SelectListItem
                         {
                             Text = x.Name,
                             Value = SqlFunctions.StringConvert((decimal)x.ID).Trim(),
                             Selected = false
                         }).ToList();

            query.Insert(0, new SelectListItem { Text = "主菜单", Value = "0", Selected = true });

            return query;
        }

        public bool CheckMenuRole(WxMenuEntity entity, string key)
        {
            var query = from x in this.DbSet
                        where x.UserName == entity.UserName
                        select x;

            if (entity.HighId == 0)
            {
                if (key == "add")
                {
                    query = from x in query
                            where x.HighId == 0
                            select x;
                }
                else if (key == "edit")
                {
                    query = from x in query
                            where x.HighId == 0
                            && x.ID != entity.ID
                            select x;
                }

                if (query.Count() >= 3)
                {
                    return false;
                }
            }
            else if (entity.HighId > 0)
            {


                if (key == "add")
                {
                    query = from x in query
                            where x.HighId == entity.HighId
                            select x;
                }
                else if (key == "edit")
                {
                    query = from x in query
                            where x.HighId == entity.HighId
                            && x.ID != entity.ID
                            select x;
                }

                if (query.Count() >= 5)
                {
                    return false;
                }
            }

            return true;
        }

        public string Use(string username)
        {
            var query = (from x in this.DbSet
                         where x.HighId == 0
                         && x.UserName == username
                         select x).ToList();

            if (query.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"button\":[");
                foreach (var first in query)
                {
                    var children = (from x in this.DbSet
                                    where x.HighId > 0
                                    && x.HighId == first.ID
                                    && x.UserName == username
                                    select x).ToList();

                    if (children.Count > 0)
                    {
                        sb.Append("{");
                        sb.AppendFormat("\"{0}\":\"{1}\",", "name", first.Name);
                        sb.Append("\"sub_button\":[");

                        StringBuilder sbChildren = new StringBuilder();
                        foreach (var child in children)
                        {
                            sbChildren.Append("{");
                            sbChildren.AppendFormat("\"{0}\":\"{1}\",", "type", first.Type);
                            sbChildren.AppendFormat("\"{0}\":\"{1}\",", "name", first.Name);
                            sbChildren.AppendFormat("\"{0}\":\"{1}\"", first.KeyOrUrl.Contains("http://") ? "url" : "key", first.KeyOrUrl);
                            sbChildren.Append("},");
                        }
                        sb.Append(sbChildren.ToString().Trim(new char[] { ',' }));
                        sb.Append("]");
                        sb.Append("},");
                    }
                    else
                    {
                        sb.Append("{");
                        sb.AppendFormat("\"{0}\":\"{1}\",", "type", first.Type);
                        sb.AppendFormat("\"{0}\":\"{1}\",", "name", first.Name);
                        sb.AppendFormat("\"{0}\":\"{1}\"", first.KeyOrUrl.Contains("http://") ? "url" : "key", first.KeyOrUrl);
                        sb.Append("},");
                    }
                }

                string json = "body=" + sb.ToString().Trim(new char[] { ',' }) + "]}";

                var user = (from x in this.DB.Set<WxAccountEntity>()
                            where x.UserName == username
                            select x).FirstOrDefault();

                string url = string.Format("{0}curd.ashx?key=C001&un={1}&unkey={2}",
                    ConfigCommonHelper.GetFromAppSettings("WeiXin"),
                    user.UserName,
                    CryptHelper.MD5(user.UserName + user.Token)
                    );

                // 调用接口
                string result = WebHelper.GetFormWebRequest(url, "POST", json);
                try
                {
                    ErrorEntity error = JsonHelper.Deserialize<ErrorEntity>(result);
                    return error.errmsg;
                }
                catch
                {
                    return "应用失败";
                }
            }

            return "没有菜单数据";
        }
    }
}