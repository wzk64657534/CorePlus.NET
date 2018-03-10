using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class AdgroupRepository : ApiRepository<AdgroupInfoEntity>
    {
        public override AdgroupInfoEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.Status = 31;
            entity.Pause = false;
            return entity;
        }

        protected override void BeforeAdd(AdgroupInfoEntity entity)
        {
            base.BeforeAdd(entity);

            entity.ExactNegativeWords = string.IsNullOrEmpty(entity.ExactNegativeWords) ? null : entity.ExactNegativeWords.Replace("\r\n", "||");
            entity.NegativeWords = string.IsNullOrEmpty(entity.NegativeWords) ? null : entity.NegativeWords.Replace("\r\n", "||");
        }
    }
}