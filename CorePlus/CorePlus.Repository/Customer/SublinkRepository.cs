using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class SublinkRepository : ApiRepository<SublinkInfoEntity>
    {
        public override SublinkInfoEntity NewEntity(object anyObj = null)
        {
            var entity = base.NewEntity(anyObj);
            entity.Status = 61;
            return entity;
        }

        protected override void BeforeAdd(SublinkInfoEntity entity)
        {
            base.BeforeAdd(entity);

            var query = (from x in this.DB.Set<AdgroupInfoEntity>()
                         where x.ID == entity.AdgroupId
                         select x).FirstOrDefault();

            if (query != null)
            {
                entity.CampaignId = query.CampaignId;
            }

            if (!string.IsNullOrEmpty(entity.Description)
                && !string.IsNullOrEmpty(entity.DescriptionUrl)
                && entity.Description.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Length
                == entity.DescriptionUrl.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Length)
            {
                string[] aryDescription = entity.Description.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] aryDescriptionUrl = entity.DescriptionUrl.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < aryDescription.Length; i++)
                {
                    entity.SublinkInfos += string.Format("{0}**{1}||", aryDescription[i], aryDescriptionUrl[i]);
                }

                entity.SublinkInfos = entity.SublinkInfos.Trim(new char[] { '|' });
            }
        }
    }
}