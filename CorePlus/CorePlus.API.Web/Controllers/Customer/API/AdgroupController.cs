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
    public class AdgroupController : ApiController<AdgroupRepository, AdgroupInfoEntity, ViewAdgroupInfoEntity>
    {
        protected override string GetWhereForSql(long? highId, long[] ids, string name, int state, bool isExact, int? cost, int? price, int? clicked)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (highId != null)
            {
                sbWhere.AppendFormat(" AND CampaignId = {0} ", highId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                sbWhere.AppendFormat(" AND Name {0} ", isExact ? (" = '" + name + "' ") : (" LIKE '%" + name + "%' "));
            }
            if (state > 0)
            {
                sbWhere.AppendFormat(" AND Status = {0} ", state);
            }

            return sbWhere.ToString();
        }

        protected override List<AdgroupInfoEntity> GetEntityList(string[] sqls, out int records)
        {
            var query = Repository.GetByPagerQuery(sqls, out records);
            return query;
        }

        protected override void FillStatistics(AdgroupInfoEntity entity, ViewAdgroupInfoEntity statistics)
        {
            statistics.ID = entity.ID;
            statistics.AccountName = entity.AccountName;
            statistics.CampaignId = entity.CampaignId ?? 0;
            statistics.CampaignName = EntityWebHelper.GetNameById<CampaignInfoEntity>(entity.CampaignId);
            statistics.AdgroupName = entity.Name;
            statistics.MaxPrice = entity.MaxPrice ?? 0;
            statistics.NegativeWords = GetTotalNegativeWords(new string[] { entity.NegativeWords, entity.ExactNegativeWords }).ToString();
            statistics.Pause = ParamWebHelper.GetDiscriptionById(3, entity.Pause.ToString());
            statistics.Status = ParamWebHelper.GetDiscriptionById(7, entity.Status.ToString());
        }

        protected override ViewStatisticsEntity GetSumOfStatistics(long id, DateTime start, DateTime end)
        {
            AdgroupStatisticsRepository repository = new AdgroupStatisticsRepository();
            return repository.GetSumByAnyId(id, start, end);
        }

        protected override int GetIndexOfStatus()
        {
            return 7;
        }
    }
}