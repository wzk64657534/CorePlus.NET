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
    public class VisitRankRepository : VisitRepository<VisitBannerEntity>
    {
        public override VisitBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var first = GetDataSource(start, start).Count();
            var second = GetDataSource(end, end).Count();

            var gap = first - second;
            var changed = string.Format("{0}({1}%)", gap, second == 0 ? 0 : decimal.Round((decimal)gap / (decimal)second, 4) * 100);

            return new VisitBannerEntity { PV = first, UV = second, REMARK = changed };
        }

        public override IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            var first = GetDataSource(start, start);
            var second = GetDataSource(end, end);

            var query = from x in
                            (from f in first
                             where string.IsNullOrEmpty(title) ? true : f.VisitingUrl.Contains(title)
                             group f by f.VisitingUrl into g
                             select new { VisitingUrl = g.Key, Count = g.Count() })
                        join y in
                            (from s in second
                             where string.IsNullOrEmpty(title) ? true : s.VisitingUrl.Contains(title)
                             group s by s.VisitingUrl into g
                             select new { VisitingUrl = g.Key, Count = g.Count() })
                                       on x.VisitingUrl equals y.VisitingUrl into xy
                        from xys in xy.DefaultIfEmpty(new { x.VisitingUrl, Count = 0 })
                        select new
                        {
                            VisitingUrl = x.VisitingUrl,
                            First = x.Count,
                            Second = xys.Count,
                            Changed = SqlFunctions.StringConvert((decimal)(x.Count - xys.Count))
                                        + (xys.Count == 0
                                                    ? "(-)"
                                                    : "(" + SqlFunctions.StringConvert(decimal.Round((decimal)(x.Count - xys.Count) / (decimal)xys.Count, 4) * 100) + "%)")
                        };

            recordCount = query.Count();
            return query.OrderByDescending(x => x.VisitingUrl).Skip((page - 1) * rows).Take(rows);
        }
    }
}