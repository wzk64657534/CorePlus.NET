using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class KeywordRepository : ApiRepository<KeywordInfoEntity>
    {
        public override KeywordInfoEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.Status = 41;
            return entity;
        }

        protected override void BeforeAdd(KeywordInfoEntity entity)
        {
            base.BeforeAdd(entity);

            var query = (from x in this.DB.Set<AdgroupInfoEntity>()
                         where x.ID == entity.AdgroupId
                         select x).FirstOrDefault();

            if (query != null)
            {
                entity.CampaignId = query.CampaignId;
            }
        }
    }
}