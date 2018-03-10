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
    public class VisitPageRepository : VisitRepository<VisitBannerEntity>
    {
        public override IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            var query = GetDataSource(start, end);
            var uid = long.Parse(CookieHelper.GetCookie("UserId"));
            var sitekeyword = (from x in this.DB.Set<UserInfoEntity>()
                               where x.ID == uid
                               select x.SiteKeyword).FirstOrDefault() ?? "";
            switch (tag)
            {
                case 1:
                    var tag1 = from x in query
                               where string.IsNullOrEmpty(title) ? true : x.VisitingUrl.Contains(title)
                               group x by x.VisitingUrl into g
                               select new
                               {
                                   VisitingUrl = g.Key,
                                   PV = g.Count(),
                                   UV = g.Select(x => x.VisitId).Distinct().Count(),
                                   IP = g.Select(x => x.LocationIP).Distinct().Count(),
                                   PvAvg = decimal.Round(g.Select(x => x.VisitId).Distinct().Count() == 0
                                       ? 0
                                       : ((decimal)g.Count()
                                               / (decimal)g.Select(x => x.VisitId).Distinct().Count()), 2),
                                   OutPV = g.Count(t => t.RefererPage == g.Key),
                                   TotalTimePeriod = g.Sum(x => x.VisitPeriodTime ?? 0) == 0
                                        ? "0时0分0秒"
                                        : (SqlFunctions.StringConvert((decimal)(g.Sum(x => x.VisitPeriodTime ?? 0) / 1000 / 3600)).Trim() + "时")
                                        + (SqlFunctions.StringConvert((decimal)(g.Sum(x => x.VisitPeriodTime ?? 0) / 1000 % 3600 / 60)).Trim() + "分")
                                        + (SqlFunctions.StringConvert((decimal)(g.Sum(x => x.VisitPeriodTime ?? 0) / 1000 % 3600 % 60)).Trim() + "秒"),
                                   AvgTimePeriod = g.Count() == 0
                                       ? "0时0分0秒"
                                       : (SqlFunctions.StringConvert((decimal)(g.Sum(x => x.VisitPeriodTime ?? 0) / 1000 / g.Count() / 3600)).Trim() + "时")
                                       + (SqlFunctions.StringConvert((decimal)(g.Sum(x => x.VisitPeriodTime ?? 0) / 1000 / g.Count() % 3600 / 60)).Trim() + "分")
                                       + (SqlFunctions.StringConvert((decimal)(g.Sum(x => x.VisitPeriodTime ?? 0) / 1000 / g.Count() % 3600 % 60)).Trim() + "秒")
                               };
                    recordCount = tag1.Count();
                    return tag1.OrderByDescending(x => x.VisitingUrl).Skip((page - 1) * rows).Take(rows);   
                case 2:
                    var q = from x in query
                            where string.IsNullOrEmpty(title) ? true : x.VisitingUrl.Contains(title)
                            select x;

                    var global = from x in this.DB.Set<VisitInfoEntity>()
                                 where string.IsNullOrEmpty(title) ? true : x.VisitingUrl.Contains(title)
                                 group x by x.VisitingUrl into g
                                 select new { VisitingUrl = g.Key, GlobalCnt = g.Count(x => x.VisitingSite != x.RefererSite) };

                    var tag2 = from x in q
                               join y in global
                               on x.VisitingUrl equals y.VisitingUrl into xy
                               from xys in xy.DefaultIfEmpty()
                               group new { x, xys } by new { x.VisitingUrl, xys.GlobalCnt } into g
                               select new
                               {
                                   VisitingUrl = g.Key.VisitingUrl,

                                   PV = g.Count(),

                                   InCnt = g.Count(t => t.x.VisitingSite != t.x.RefererSite),

                                   //JumpOutRate = 0,

                                   Deep = g.Count(t => t.x.VisitingSite != t.x.RefererSite) == 0
                                               ? 0
                                               : decimal.Round((decimal)(from t in query
                                                                         where t.LoginPage == g.Key.VisitingUrl
                                                                         select t).Count() / (decimal)g.Count(t => t.x.VisitingSite != t.x.RefererSite), 2),

                                   AvgTimePeriod = !g.Select(t => t.x.VisitId).Distinct().Any()
                                        ? "0时0分0秒"
                                        : (SqlFunctions.StringConvert((decimal)(g.Sum(t => t.x.VisitPeriodTime) / 1000 / (decimal)g.Select(t => t.x.VisitId).Distinct().Count() / 3600)).Trim() + "时")
                                        + (SqlFunctions.StringConvert((decimal)(g.Sum(t => t.x.VisitPeriodTime) / 1000 / (decimal)g.Select(t => t.x.VisitId).Distinct().Count() % 3600 / 60)).Trim() + "分")
                                        + (SqlFunctions.StringConvert((decimal)(g.Sum(t => t.x.VisitPeriodTime) / 1000 / (decimal)g.Select(t => t.x.VisitId).Distinct().Count() % 3600 % 60)).Trim() + "秒"),

                                   InCntRate = g.Key.GlobalCnt == 0
                                   ? 0
                                   : decimal.Round((decimal)g.Count(t => t.x.VisitingSite != t.x.RefererSite) / (decimal)g.Key.GlobalCnt, 4) * 100
                               };

                    recordCount = tag2.Count();
                    return tag2.OrderByDescending(x => x.VisitingUrl).Skip((page - 1) * rows).Take(rows);
                default:
                    recordCount = 0;
                    return null;
            }
        }
    }
}