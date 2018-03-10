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
    public class CampaignController : ApiController<CampaignRepository, CampaignInfoEntity, ViewCampaignInfoEntity>
    {
        protected override string GetWhereForSql(long? highId, long[] ids, string name, int state, bool isExact, int? cost, int? price, int? clicked)
        {
            StringBuilder sbWhere = new StringBuilder();

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

        protected override List<CampaignInfoEntity> GetEntityList(string[] sqls, out int records)
        {
            return Repository.GetByPagerQuery(sqls, out records).ToList();
        }

        protected override void FillStatistics(CampaignInfoEntity entity, ViewCampaignInfoEntity statistics)
        {
            statistics.ID = entity.ID;
            statistics.AccountName = entity.AccountName;
            statistics.CampaignName = entity.Name;
            statistics.Budget = entity.Budget.ToString();
            // 计划推广地域和账户推广地域比对，相同用的就是账户地域，否则是计划地域
            statistics.RegionTarget = string.IsNullOrEmpty(statistics.RegionTarget) ? "" : GetRegionTarget(entity.RegionTarget);
            // 只需要个数
            statistics.ExcludeIp = string.IsNullOrEmpty(statistics.ExcludeIp) ? "" : entity.ExcludeIp.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries).Length.ToString();
            // 2个字段的个数求和
            statistics.NegativeWords = string.IsNullOrEmpty(statistics.NegativeWords) ? "" : GetTotalNegativeWords(new string[] { entity.NegativeWords, entity.ExactNegativeWords }).ToString();
            // 推广时段，检查数据中包含数字0-24
            bool IsScheduleAll = true;
            if (!string.IsNullOrEmpty(entity.Schedule))
            {
                string[] schedules = entity.Schedule.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var hour in ParamWebHelper.Hours)
                {
                    foreach (var schedule in schedules)
                    {
                        if (schedule.IndexOf(hour) < 0)
                        {
                            IsScheduleAll = false;
                            break;
                        }
                    }

                    if (!IsScheduleAll) { break; }
                }
            }

            statistics.Schedule = IsScheduleAll ? "全时段" : "自定义";

            statistics.BudgetOfflineTime = entity.BudgetOfflineTime;
            statistics.ShowProb = ParamWebHelper.GetDiscriptionById(6, entity.ShowProb.ToString());
            statistics.Pause = ParamWebHelper.GetDiscriptionById(3, entity.Pause.ToString());
            statistics.JoinContent = ParamWebHelper.GetDiscriptionById(4, entity.JoinContent.ToString());
            statistics.ContentPrice = entity.ContentPrice.ToString();
            statistics.Status = ParamWebHelper.GetDiscriptionById(5, entity.Status.ToString());
        }

        protected override ViewStatisticsEntity GetSumOfStatistics(long id, DateTime start, DateTime end)
        {
            CampaignStatisticsRepository repository = new CampaignStatisticsRepository();
            return repository.GetSumByAnyId(id, start, end);
        }

        protected override int GetIndexOfStatus()
        {
            return 5;
        }
    }
}