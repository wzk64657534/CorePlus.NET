using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class CampaignRepository : ApiRepository<CampaignInfoEntity>
    {
        public override CampaignInfoEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.Budget = 0;
            entity.Status = 21;
            entity.JoinContent = false;
            entity.ContentPrice = 0;
            entity.ShowProb = 1;
            entity.Pause = false;
            return entity;
        }

        protected override void BeforeAdd(CampaignInfoEntity entity)
        {
            base.BeforeAdd(entity);
            entity.RegionTarget = entity.RegionList == null ? null : string.Join(",", entity.RegionList);
            entity.ExactNegativeWords = string.IsNullOrEmpty(entity.ExactNegativeWords) ? null : entity.ExactNegativeWords.Replace("\r\n", "||");
            entity.ExcludeIp = string.IsNullOrEmpty(entity.ExcludeIp) ? null : entity.ExcludeIp.Replace("\r\n", "||");
            entity.NegativeWords = string.IsNullOrEmpty(entity.NegativeWords) ? null : entity.NegativeWords.Replace("\r\n", "||");
        }
    }
}