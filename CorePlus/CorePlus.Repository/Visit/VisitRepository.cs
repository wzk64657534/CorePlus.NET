using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Core;
using CorePlus.Entity;
using CorePlus.Repository;
using System.Data.Objects.SqlClient;

namespace CorePlus.Repository
{
    public class VisitRepository<TBannerEntity> : BaseRepository<VisitInfoEntity>
        where TBannerEntity : VisitBannerEntity, new()
    {
        public virtual TBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            // 浏览次数
            var pv = (int)GetPV(start, end).Data;

            // 独立访客数
            var uv = (int)GetUV(start, end).Data;

            // IP
            var ip = (int)GetIP(start, end).Data;

            // 人均浏览页数
            var pvavg = uv == 0 ? 0 : decimal.Round((decimal)pv / (decimal)uv, 2);

            // 全站总PV
            var totalpv = GetTotalPV().Data ?? 0;

            // 浏览次数占比
            var pvpercent = totalpv == 0 ? 0 : decimal.Round((decimal)pv / totalpv, 2) * 100;

            var t = (int)GetPeriodTime(start, end).Data;
            var totaltime = t == 0 ? "0时0分0秒" : string.Format("{0}时{1}分{2}秒", t / 3600, t % 3600 / 60, t % 3600 % 60);

            var avgt = (int)GetAvgPeriodTime(start, end).Data; //t / pv;
            var avgtime = avgt == 0 ? "0时0分0秒" : string.Format("{0}时{1}分{2}秒", avgt / 3600, avgt % 3600 / 60, avgt % 3600 % 60);

            return new TBannerEntity
            {
                PV = pv,
                UV = uv,
                IP = ip,
                PVAVG = pvavg,
                TOTALPV = totalpv,
                PVPERCENT = pvpercent,
                TOTALTIME = totaltime,
                AVGTIME = avgtime
            };
        }

        public virtual IQueryable<VisitInfoEntity> GetDataSource(DateTime start, DateTime end)
        {
            long uid = long.Parse(CookieHelper.GetCookie("UserId") ?? "0");
            // 源数据
            end = end.AddDays(1);
            var query = from x in this.DbSet
                        where x.UserId == uid
                        && x.VisitTime >= start
                        && x.VisitTime < end
                        select x;

            return query;
        }

        public virtual IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            recordCount = 0;

            var query = GetDataSource(start, end);
            var result = (from x in query
                          where string.IsNullOrEmpty(title) ? true : x.VisitingUrl.Contains(title)
                          select x).OrderByDescending(x => x.VisitTime).Skip((page - 1) * rows).Take(rows);
            return result;
        }

        public ChartItemEntity GetPV(DateTime start, DateTime end)
        {
            var query = GetDataSource(start, end);
            var pv = (from x in query
                      select x).Count();

            return new ChartItemEntity { Name = "PV", Data = pv };
        }

        public ChartItemEntity GetUV(DateTime start, DateTime end)
        {
            var query = GetDataSource(start, end);
            var uv = (from x in query
                      group x by new
                      {
                          Year = SqlFunctions.DateName("yyyy", x.VisitTime),
                          Month = SqlFunctions.DateName("MM", x.VisitTime),
                          Day = SqlFunctions.DateName("dd", x.VisitTime),
                          x.VisitId
                      } into g
                      select new
                      {
                          Name = g.Key.Year + "-" + g.Key.Month + "-" + g.Key.Day + "-" + g.Key.VisitId,
                          Data = g.Count()
                      }).Count();

            return new ChartItemEntity { Name = "UV", Data = uv };
        }

        /// <summary>
        /// 独立新访客
        /// </summary>
        public ChartItemEntity GetNewUV(DateTime start, DateTime end)
        {
            var ed = end.AddDays(1);
            var onlyOne = from x in this.DbSet
                          where (x.VisitTime < ed)
                          group x by x.VisitId into g
                          where g.Count() == 1
                          select new
                          {
                              VisitId = g.Key
                          };

            var query = GetDataSource(start, end);

            var newuv = (from x in query
                         where (from y in onlyOne
                                where y.VisitId == x.VisitId
                                select y).Any()
                         group x by new
                         {
                             Year = SqlFunctions.DateName("yyyy", x.VisitTime),
                             Month = SqlFunctions.DateName("MM", x.VisitTime),
                             Day = SqlFunctions.DateName("dd", x.VisitTime),
                             x.VisitId
                         } into g
                         select new
                         {
                             Name = g.Key.Year + "-" + g.Key.Month + "-" + g.Key.Day + "-" + g.Key.VisitId,
                             Data = g.Count()
                         }).Count();

            return new ChartItemEntity { Name = "NEWUV", Data = newuv };
        }

        public ChartItemEntity GetIP(DateTime start, DateTime end)
        {
            var query = GetDataSource(start, end);
            var ip = (from x in query
                      group x by new
                      {
                          Year = SqlFunctions.DateName("yyyy", x.VisitTime),
                          Month = SqlFunctions.DateName("MM", x.VisitTime),
                          Day = SqlFunctions.DateName("dd", x.VisitTime),
                          x.LocationIP
                      } into g
                      select new ChartItemEntity
                      {
                          Name = g.Key.Year + "-" + g.Key.Month + "-" + g.Key.Day + "-" + g.Key.LocationIP,
                          Data = g.Count()
                      }).Count();

            return new ChartItemEntity { Name = "IP", Data = ip };
        }

        /// <summary>
        /// 访问次数
        /// </summary>
        public ChartItemEntity GetVisitCount(DateTime start, DateTime end)
        {
            var query = GetDataSource(start, end);

            var vc = (from x in query
                      where x.VisitingSite != x.RefererSite
                      select x).Count();

            return new ChartItemEntity { Name = "VC", Data = vc };
        }

        /// <summary>
        /// 全站总PV
        /// </summary>
        public ChartItemEntity GetTotalPV()
        {
            var query = (from x in this.DbSet
                         select x).Count();
            return new ChartItemEntity { Name = "TOTALPV", Data = query };
        }
        /// <summary>
        /// 全站总访问次数
        /// </summary>
        public ChartItemEntity GetTotalVisitCnt()
        {
            var query = (from x in this.DbSet
                         where x.VisitingSite != x.RefererSite
                         select x).Count();
            return new ChartItemEntity { Name = "TotalVisitCnt", Data = query };
        }
        /// <summary>
        /// 区间内停留总时长
        /// </summary>
        public ChartItemEntity GetPeriodTime(DateTime start, DateTime end)
        {
            var query = GetDataSource(start, end);
            var time = !query.Any() ? 0 : query.Sum(x => x.VisitPeriodTime ?? 0) / 1000;
            return new ChartItemEntity { Name = "PeriodTime", Data = time };
        }
        /// <summary>
        /// 平均停留时长
        /// </summary>
        public ChartItemEntity GetAvgPeriodTime(DateTime start, DateTime end)
        {
            var query = GetDataSource(start, end);
            var pvall = query.Count();
            var time = pvall == 0 ? 0 : query.Sum(x => x.VisitPeriodTime ?? 0) / query.Count() / 1000;

            return new ChartItemEntity { Name = "AvgPeriodTime", Data = time };
        }

        public virtual List<ChartDataEntity> GetDataOfChartForXY(DateTime start, DateTime end, int? sourceType)
        {
            var query = GetDataSource(start, end);

            IQueryable<ChartItemEntity> items = LogicDataSourceForXY(query, start, end, sourceType);

            List<ChartDataEntity> datas = new List<ChartDataEntity>();
            foreach (var item in items)
            {
                var entity = (from x in datas
                              where x.name == item.Name
                              select x).FirstOrDefault();

                if (entity == null)
                {
                    ChartDataEntity data = new ChartDataEntity();
                    data.name = item.Name;
                    data.data.Add(item.Data ?? 0);
                    datas.Add(data);
                }
                else
                {
                    entity.data.Add(item.Data ?? 0);
                }
            }

            return datas;
        }

        protected virtual IQueryable<ChartItemEntity> LogicDataSourceForXY(IQueryable<VisitInfoEntity> query, DateTime start, DateTime end, int? sourceType)
        {
            return Enumerable.Empty<ChartItemEntity>().AsQueryable();
        }
    }
}