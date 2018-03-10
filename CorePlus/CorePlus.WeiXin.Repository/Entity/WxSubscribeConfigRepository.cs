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
    public class WxSubscribeConfigRepository : WxUserNameRepository<WxSubscribeConfigEntity>
    {
        public override WxSubscribeConfigEntity Update(long id, WxSubscribeConfigEntity uiEntity)
        {
            var entity = FindByID(id);
            if (entity == null)
            {
                Add(uiEntity);
            }
            else
            {
                CheckNotNull(entity);
                CheckNotNull(uiEntity);
                Update(entity, uiEntity);
            }
            return entity;
        }

        public WxReplyTextEntity EditText(string username)
        {
            var entity = (from x in this.DB.Set<WxReplyTextEntity>()
                          where x.Keyword == "subscribe"
                          select x).FirstOrDefault();

            if (entity == null)
            {
                entity = new WxReplyTextEntity();
                entity.ID = 0;
                entity.Keyword = "subscribe";
                entity.UserName = username;
                entity.MatchType = 1;
            }

            return entity;
        }

        public WxReplyImageTextEntity EditImageText(string username, List<WxReplyImageTextEntity> list)
        {
            var entity = (from x in this.DB.Set<WxReplyImageTextEntity>()
                          where x.Keyword == "subscribe"
                          select x).FirstOrDefault();

            if (entity == null)
            {
                entity = new WxReplyImageTextEntity();
                entity.ID = 0;
                entity.Keyword = "subscribe";
                entity.UserName = username;
                entity.MatchType = 1;
            }
            else
            {
                if (!string.IsNullOrEmpty(entity.WithIds))
                {
                    string[] ids = entity.WithIds.Split(new char[] { ',' });
                    foreach (var id in ids)
                    {
                        var item = (from x in list
                                    where x.ID.ToString() == id
                                    select x).FirstOrDefault();
                        if (item != null) { item.Selected = true; }
                    }
                }
            }

            return entity;
        }

        public WxReplyImageTextEntity EditImageText(WxReplyImageTextEntity entity)
        {
            WxReplyImageTextRepository repository = new WxReplyImageTextRepository();
            if (entity != null && entity.Ids != null)
            {
                entity.WithIds = string.Join(",", entity.Ids);
            }

            if (entity.ID == 0)
            {

                repository.Add(entity);
            }
            else
            {
                repository.Update(entity.ID, entity);
            }

            return entity;
        }

        public List<WxReplyImageTextEntity> GetAllWithSubscribe(string username)
        {
            var query = (from x in this.DB.Set<WxReplyImageTextEntity>()
                         where x.UserName == username
                         && x.Keyword != "subscribe"
                         select x).ToList();

            return query;
        }
    }
}