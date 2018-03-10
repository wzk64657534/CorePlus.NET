using System;
using System.Data.Objects.SqlClient;
using System.Linq;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class ResourceRankRepository : VisitRepository<ResourceClassificationBannerEntity>
    {
        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var first = GetDataSource(start, start).Count(x => x.RefererSite != x.VisitingSite);
            var second = GetDataSource(end, end).Count(x => x.RefererSite != x.VisitingSite);

            var gap = first - second;
            var changed = string.Format("{0}({1}%)", gap, second == 0 ? 0 : decimal.Round(gap / (decimal)second, 4) * 100);

            return new ResourceClassificationBannerEntity { PV = first, UV = second, REMARK = changed };
        }

        public override IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            var first = GetDataSource(start, start).Where(x => x.RefererSite != x.VisitingSite);
            var second = GetDataSource(end, end).Where(x => x.RefererSite != x.VisitingSite);

            var query = from x in
                            (from f in first
                             //where string.IsNullOrEmpty(title) ? true : f.VisitingUrl.Contains(title)
                             group f by f.RefererSite into g
                             select new { RefererSite = g.Key, Count = g.Count() })
                        join y in
                            (from s in second
                             //where string.IsNullOrEmpty(title) ? true : s.VisitingUrl.Contains(title)
                             group s by s.RefererSite into g
                             select new { RefererSite = g.Key, Count = g.Count() })
                                       on x.RefererSite equals y.RefererSite into xy
                        from xys in xy.DefaultIfEmpty(new { x.RefererSite, Count = 0 })
                        select new
                        {
                            x.RefererSite,
                            First = x.Count,
                            Second = xys.Count,
                            Changed = SqlFunctions.StringConvert((decimal)(x.Count - xys.Count))
                                        + (xys.Count == 0
                                                    ? "(-)"
                                                    : "(" + SqlFunctions.StringConvert(decimal.Round((x.Count - xys.Count) / (decimal)xys.Count, 4) * 100) + "%)")
                        };

            recordCount = query.Count();
            return query.OrderByDescending(x => x.RefererSite).Skip((page - 1) * rows).Take(rows);
        }
    }
}