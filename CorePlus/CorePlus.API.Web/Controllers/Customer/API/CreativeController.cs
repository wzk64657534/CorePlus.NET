using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.API.Web
{
    public class CreativeController : ApiController<CreativeRepository, CreativeInfoEntity, ViewCreativeInfoEntity>
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

        protected override List<CreativeInfoEntity> GetEntityList(string[] sqls, out int records)
        {
            return Repository.GetByPagerQuery(sqls, out records).ToList();
        }

        protected override void FillStatistics(CreativeInfoEntity entity, ViewCreativeInfoEntity statistics)
        {
            statistics.ID = entity.ID;
            statistics.AccountName = entity.AccountName;
            statistics.CampaignId = entity.CampaignId ?? 0;
            statistics.CampaignName = EntityWebHelper.GetNameById<CampaignInfoEntity>(entity.CampaignId ?? 0);
            statistics.AdgroupId = entity.AdgroupId ?? 0;
            statistics.AdgroupName = EntityWebHelper.GetNameById<AdgroupInfoEntity>(entity.AdgroupId ?? 0);
            // 拼装Title
            StringBuilder sbTitle = new StringBuilder();
            sbTitle.Append(GetColorfulInfo("<a href='#' onclick='javascript:void(0);' style='font-size:14px;text-decoration:underline;'>" + entity.Title + "</a>"));
            sbTitle.Append("<br/>");
            sbTitle.Append(GetColorfulInfo(entity.Description1));
            sbTitle.Append(GetColorfulInfo(entity.Description2));
            sbTitle.Append("<br/>");
            sbTitle.AppendFormat("<a href='{1}' alt=''>{0}</a>", entity.DisplayUrl, entity.DestinationUrl);
            statistics.Title = sbTitle.ToString();
            statistics.Pause = ParamWebHelper.GetDiscriptionById(3, entity.Pause.ToString());
            statistics.Status = ParamWebHelper.GetDiscriptionById(11, entity.Status.ToString());
        }

        protected override ViewStatisticsEntity GetSumOfStatistics(long id, DateTime start, DateTime end)
        {
            KeywordStatisticsRepository repository = new KeywordStatisticsRepository();
            return repository.GetSumByAnyId(id, start, end);
        }

        private string GetColorfulInfo(string strValue)
        {
            return strValue.Replace("{", "<span style='background-color:yellow;'>").Replace("}", "</span>");
        }
    }
}