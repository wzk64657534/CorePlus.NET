using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Core;
using CorePlus.Entity;
using CorePlus.Repository;

namespace CorePlus.Web
{
    public class KeywordController : ApiBaseController<KeywordRepository, KeywordInfoEntity, ViewKeywordInfoEntity>
    {
        protected override string GetWhereForSql(long? highId, long[] ids, string name, int state, bool isExact, int? cost, int? price, int? clicked)
        {
            StringBuilder sbWhere = new StringBuilder();
            if (highId != null)
            {
                sbWhere.AppendFormat(" AND AdgroudId = {0} ", highId);
            }
            if (ids != null)
            {
                if (ids.Length == 1)
                {
                    sbWhere.AppendFormat(" AND ID = {0} ", ids[0]);
                }
                else
                {
                    sbWhere.Append(" AND ID IN ( ");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (i == (ids.Length - 1))
                        {
                            sbWhere.AppendFormat(" {0} ", ids[i]);
                        }
                        else
                        {
                            sbWhere.AppendFormat(" {0}, ", ids[i]);
                        }
                    }
                    sbWhere.Append(" ) ");
                }
            }
            if (!string.IsNullOrEmpty(name))
            {
                sbWhere.AppendFormat(" AND Keyword {0} ", isExact ? (" = '" + name + "' ") : (" LIKE '%" + name + "%' "));
            }
            if (state > 0)
            {
                sbWhere.AppendFormat(" AND Status = {0} ", state);
            }
            if (cost != null)
            {
                switch (cost)
                {
                    case 0:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT B.KeywordId, B.TotalCost FROM dbo.KeywordStatistics AS B ");
                        sbWhere.Append(" WHERE M.ID = B.KeywordId ");
                        sbWhere.Append(" AND B.TotalCost = 0) ");
                        break;
                    case 1:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT TOP 20 PERCENT B.KeywordId, B.TotalCost FROM dbo.KeywordStatistics AS B ");
                        sbWhere.Append(" WHERE M.ID = B.KeywordId ");
                        sbWhere.Append(" ORDER BY B.TotalCost ASC) ");
                        break;
                    case 2:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT TOP 20 PERCENT B.KeywordId, B.TotalCost FROM dbo.KeywordStatistics AS B ");
                        sbWhere.Append(" WHERE M.ID = B.KeywordId ");
                        sbWhere.Append(" ORDER BY B.TotalCost DESC) ");
                        break;
                }
            }
            if (clicked != null)
            {
                switch (clicked)
                {
                    case 0:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT C.KeywordId,C.Clicked FROM dbo.KeywordStatistics AS C ");
                        sbWhere.Append(" WHERE M.ID = C.KeywordId ");
                        sbWhere.Append(" AND C.Clicked = 0) ");
                        break;
                    case 1:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT TOP 20 PERCENT C.KeywordId,C.Clicked FROM dbo.KeywordStatistics AS C ");
                        sbWhere.Append(" WHERE M.ID = C.KeywordId ");
                        sbWhere.Append(" ORDER BY C.Clicked ASC) ");
                        break;
                    case 2:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT TOP 20 PERCENT C.KeywordId,C.Clicked FROM dbo.KeywordStatistics AS C ");
                        sbWhere.Append(" WHERE M.ID = C.KeywordId ");
                        sbWhere.Append(" ORDER BY C.Clicked DESC) ");
                        break;
                }
            }
            if (price != null)
            {
                switch (price)
                {
                    case 1:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT TOP 20 PERCENT D.KeywordId, D.AvgClickedPrice FROM dbo.KeywordStatistics AS D ");
                        sbWhere.Append(" WHERE M.ID = D.KeywordId ");
                        sbWhere.Append(" ORDER BY D.AvgClickedPrice ASC) ");
                        break;
                    case 2:
                        sbWhere.Append(" AND EXISTS(SELECT DISTINCT TOP 20 PERCENT D.KeywordId, D.AvgClickedPrice FROM dbo.KeywordStatistics AS D ");
                        sbWhere.Append(" WHERE M.ID = D.KeywordId ");
                        sbWhere.Append(" ORDER BY D.AvgClickedPrice DESC) ");
                        break;
                }
            }

            return sbWhere.ToString();
        }

        protected override List<KeywordInfoEntity> GetEntityList(string[] sqls, out int records)
        {
            return Repository.GetByPagerQuery(sqls, out records).ToList();
        }

        protected override void FillStatistics(KeywordInfoEntity entity, ViewKeywordInfoEntity statistics)
        {
            statistics.ID = entity.ID;
            statistics.AccountName = entity.AccountName;
            statistics.CampaignId = entity.CampaignId ?? 0;
            statistics.CampaignName = EntityWebHelper.GetNameById<CampaignInfoEntity>(entity.CampaignId);
            statistics.AdgroupId = entity.AdgroupId ?? 0;
            statistics.AdgroupName = EntityWebHelper.GetNameById<AdgroupInfoEntity>(entity.AdgroupId);
            statistics.Keyword = entity.Keyword;
            statistics.Price = entity.Price ?? 0;
            statistics.DestinationUrl = entity.DestinationUrl;
            statistics.MatchType = ParamWebHelper.GetDiscriptionById(8, entity.MatchType.ToString());
            statistics.Quality = ParamWebHelper.GetDiscriptionById(10, entity.Quality.ToString());
            statistics.Pause = ParamWebHelper.GetDiscriptionById(3, entity.Pause.ToString());
            statistics.Status = ParamWebHelper.GetDiscriptionById(9, entity.Status.ToString());
        }

        protected override ViewStatisticsEntity GetSumOfStatistics(long id, DateTime start, DateTime end)
        {
            KeywordStatisticsRepository repository = new KeywordStatisticsRepository();
            return repository.GetSumByAnyId(id, start, end);
        }

        protected override int GetIndexOfStatus()
        {
            return 9;
        }
    }
}