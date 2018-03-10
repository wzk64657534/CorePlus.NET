using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web.Controllers
{
    public class SublinkController : ApiBaseController<SublinkRepository, SublinkInfoEntity, ViewSublinkInfoEntity>
    {
        protected override string GetWhereForSql(long? highId, long[] ids, string name, int state, bool isExact, int? cost, int? price, int? clicked)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (highId != null)
            {
                sbWhere.AppendFormat(" AND AdgroupId = {0} ", highId);
            }

            return sbWhere.ToString();
        }

        protected override List<SublinkInfoEntity> GetEntityList(string[] sqls, out int records)
        {
            return Repository.GetByPagerQuery(sqls, out records).ToList();
        }

        protected override void FillStatistics(SublinkInfoEntity entity, ViewSublinkInfoEntity statistics)
        {
            statistics.ID = entity.ID;
            statistics.AccountName = entity.AccountName;
            statistics.CampaignId = entity.CampaignId ?? 0;
            statistics.CampaignName = EntityWebHelper.GetNameById<CampaignInfoEntity>(entity.CampaignId);
            statistics.AdgroupId = entity.AdgroupId ?? 0;
            statistics.AdgroupName = EntityWebHelper.GetNameById<AdgroupInfoEntity>(entity.AdgroupId);
            // 拼装Title
            StringBuilder sbTitle = new StringBuilder();
            string[] subinfos = entity.SublinkInfos.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var subinfo in subinfos)
            {
                if (!string.IsNullOrEmpty(subinfo.Replace("-**-", "")))
                {
                    string[] fields = subinfo.Split(new string[] { "**" }, StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length > 0)
                    {
                        sbTitle.AppendFormat("<a href='#' onclick='javascript:void(0);' style='text-decoration:underline;'>{0}</a>&nbsp;", fields[0]);
                    }
                }
            }
            statistics.SublinkInfos = sbTitle.ToString();
            statistics.Pause = ParamWebHelper.GetDiscriptionById(3, entity.Pause.ToString());
            statistics.Status = ParamWebHelper.GetDiscriptionById(12, entity.Status.ToString());
        }
    }
}