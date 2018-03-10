using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using CorePlus.WeiXin.Entity;

namespace CorePlus.WeiXin.Repository
{
    public class WxReplyImageTextRepository : WxReplyBaseRepository<WxReplyImageTextEntity>
    {
        protected override void BeforeAdd(WxReplyImageTextEntity entity)
        {
            if (entity.Ids != null)
            {
                entity.WithIds = string.Join(",", entity.Ids);
            }

            base.BeforeAdd(entity);
        }

        protected override void BeforeUpdate(WxReplyImageTextEntity entity, WxReplyImageTextEntity uiEntity)
        {
            if (uiEntity.Ids != null)
            {
                uiEntity.WithIds = string.Join(",", uiEntity.Ids);
            }

            base.BeforeUpdate(entity, uiEntity);
        }

        public List<WxReplyImageTextEntity> CheckSelected(string username, long id, WxReplyImageTextEntity entity)
        {
            var list = FindAllOfSelf(username).Where(x => x.ID != id && x.Keyword != "subscribe").ToList();
            if (!string.IsNullOrEmpty(entity.WithIds))
            {
                string[] ids = entity.WithIds.Split(new char[] { ',' });
                foreach (var Id in ids)
                {
                    var item = (from x in list
                                where x.ID.ToString() == Id
                                select x).FirstOrDefault();
                    if (item != null) { item.Selected = true; }
                }
            }

            return list;
        }

        public List<WxReplyImageTextEntity> FindAllOfSelf(string username)
        {
            return FindAll().Where(x => x.UserName == username && x.Keyword != "subscribe").ToList();
        }
    }
}