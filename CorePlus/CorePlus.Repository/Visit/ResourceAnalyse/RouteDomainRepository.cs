using System;
using System.Linq;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class RouteDomainRepository : VisitRepository<ResourceClassificationBannerEntity>
    {

        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var entity = base.GetDataOfBanner(start, end);
            var data = GetNewUV(start, end).Data;
            var count = GetVisitCount(start, end).Data;
            if (count != null) entity.VISITCOUNT = (int)count;
            if (data != null) entity.NEWUV = (int)data;
            return entity;
        }

        public override IQueryable<VisitInfoEntity> GetDataSource(DateTime start, DateTime end)
        {
            var query = base.GetDataSource(start, end);
            return query;
        }

        public override IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            var query = GetDataSource(start, end);

            var temp = from x in query
                       where string.IsNullOrEmpty(title) || x.RefererSite.Contains(title) && !string.IsNullOrEmpty(x.RefererSite)
                       group x by x.RefererSite into g
                       select new
                       {
                           RefererSite = g.Key,
                           PV = g.Count(),
                           UV = g.Select(x => x.VisitId).Distinct().Count(),
                           IP = g.Select(x => x.LocationIP).Distinct().Count(),
                           VisitCount = g.Select(x => x.VisitingSite != x.RefererSite).Count(),
                       };
            recordCount = temp.Count();
            return temp.OrderByDescending(x => x.RefererSite).Skip((page - 1) * rows).Take(rows);
        }
    }
}