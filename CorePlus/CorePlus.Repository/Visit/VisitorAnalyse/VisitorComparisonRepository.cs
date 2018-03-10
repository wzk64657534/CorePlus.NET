using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Core;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class VisitorComparisonRepository : VisitRepository<ResourceClassificationBannerEntity>
    {
        public List<VisitorComparisonEntity> GetListVisitor(DateTime start, DateTime end)
        {
            end = end.AddDays(1);
            var list = new List<VisitorComparisonEntity>
            {
                GetVisitorData(start, end, g => g.Count() == 1),
                GetVisitorData(start, end, g => g.Count() > 1)
            };
            return list;

        }

        public VisitorComparisonEntity GetVisitorData(DateTime start, DateTime end, Expression<Func<IGrouping<string, VisitInfoEntity>, bool>> whereExpression)
        {
            var temp =
                   DbSet.Where(x => (x.VisitTime < end))
                   .GroupBy(x => x.VisitId)
                   .Where(whereExpression)
                   .Select(g => new
                  {
                      VisitId = g.Key
                  });

            long uid = long.Parse(CookieHelper.GetCookie("UserId") ?? "0");

            var query = from x in DbSet
                        where x.UserId == uid
                        && x.VisitTime >= start
                        && x.VisitTime < end
                        && (from y in temp
                            where y.VisitId == x.VisitId
                            select y.VisitId).Any()
                        select x;

            var pv = (from x in query
                      select x).Count();

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

            //访问次数
            var vc = (from x in query
                      where x.VisitingSite != x.RefererSite
                      select x).Count();
            // 人均浏览页数
            var pvavg = uv == 0 ? 0 : decimal.Round(pv / (decimal)uv, 2);

            //平均访问时长
            var pvall = query.Count();
            var avgt = pvall == 0 ? 0 : query.Sum(x => x.VisitPeriodTime ?? 0) / query.Count() / 1000;
            //var avgtime = avgt == 0 ? "0时0分0秒" : string.Format("{0:f}时{1:f}分{2:f}秒", avgt / 3600, avgt % 3600 / 60, avgt % 3600 % 60);
            var avgtime = avgt == 0
                ? "0时0分0秒"
                : (Math.Floor(avgt / 3600)) + "时" + ((Math.Floor(avgt % 3600 / 60)) + "分") +
                  (Math.Floor(avgt % 3600 % 60) + "秒");



            return new VisitorComparisonEntity { VC = vc, PV = pv, UV = uv, PVAVG = pvavg, AVGTIME = avgtime };
        }

        //public override IQueryable<VisitInfoEntity> GetDataSource(DateTime start, DateTime end)
        //{
        //    var ed = end.AddDays(1);
        //    var onlyOne = from x in DbSet
        //                  where (x.VisitTime < ed)
        //                  group x by x.VisitId into g
        //                  where g.Count() == 1
        //                  select new
        //                  {
        //                      VisitId = g.Key
        //                  };

        //    long uid = long.Parse(CookieHelper.GetCookie("UserId") ?? "0");
        //    // 源数据
        //    end = end.AddDays(1);
        //    var query = from x in DbSet
        //                where x.UserId == uid
        //                && x.VisitTime >= start
        //                && x.VisitTime < end
        //                && (from y in onlyOne
        //                    where y.VisitId == x.VisitId
        //                    select y.VisitId).Any()
        //                select x;
        //    return query;
        //}


    }
}
