using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class StatisticRepository<TEntity> : SimpleRepository<TEntity>
        where TEntity : StatisticsEntity, new()
    {
        public virtual ViewStatisticsEntity GetSumByAnyId(long anyId, Nullable<DateTime> start = null, Nullable<DateTime> end = null)
        {
            var db = CoreDBContext.GetContext();

            var query = (from x in db.Set<TEntity>()
                         where x.AccountId == anyId
                         && (start != null ? x.SynchroDate >= start : true)
                         && (end != null ? x.SynchroDate <= end : true)
                         group x by x.AccountId into g
                         select new ViewStatisticsEntity
                         {
                             ID = g.Key ?? 0,
                             AvgClickedPrice = g.Sum(m => m.AvgClickedPrice) ?? 0,
                             Clicked = g.Sum(m => m.Clicked) ?? 0,
                             ClickedRate = g.Sum(m => m.ClickedRate) ?? 0,
                             ShowCnt = g.Sum(m => m.ShowCnt) ?? 0,
                             ThousandCost = g.Sum(m => m.ThousandCost) ?? 0,
                             TotalCost = g.Sum(m => m.TotalCost) ?? 0,
                             TransformCnt = g.Sum(m => m.TransformCnt) ?? 0
                         }).FirstOrDefault();

            return query;

        }

        public virtual List<ViewStatisticsEntity> GetSumByAnyIds(long[] anyIds, Nullable<DateTime> start = null, Nullable<DateTime> end = null)
        {
            var db = CoreDBContext.GetContext();

            var query = from x in db.Set<TEntity>()
                        where (from ids in anyIds
                               where x.AccountId == ids
                               select ids).Any()
                        && (start != null ? x.SynchroDate >= start : true)
                        && (end != null ? x.SynchroDate <= end : true)
                        group x by x.AccountId into g
                        select new ViewStatisticsEntity
                        {
                            ID = g.Key ?? 0,
                            AvgClickedPrice = g.Sum(m => m.AvgClickedPrice) ?? 0,
                            Clicked = g.Sum(m => m.Clicked) ?? 0,
                            ClickedRate = g.Sum(m => m.ClickedRate) ?? 0,
                            ShowCnt = g.Sum(m => m.ShowCnt) ?? 0,
                            ThousandCost = g.Sum(m => m.ThousandCost) ?? 0,
                            TotalCost = g.Sum(m => m.TotalCost) ?? 0,
                            TransformCnt = g.Sum(m => m.TransformCnt) ?? 0
                        };

            return query.ToList();
        }
    }
}