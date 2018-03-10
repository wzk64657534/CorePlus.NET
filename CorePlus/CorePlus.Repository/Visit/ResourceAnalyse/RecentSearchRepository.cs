using System;
using System.Data.Objects.SqlClient;
using System.Linq;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class RecentSearchRepository : VisitRepository<ResourceClassificationBannerEntity>
    {

        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var entity = base.GetDataOfBanner(start, end);

            var data = GetNewUV(start, end).Data;
            var count = GetVisitCount(start, end).Data;

            if (count != null) entity.VISITCOUNT = (int)count;
            if (data != null)entity.NEWUV = (int)data;
             
            return entity;
        }

        public override IQueryable<VisitInfoEntity> GetDataSource(DateTime start, DateTime end)
        {
            var query = base.GetDataSource(start, end);
            return query;
        }

        public override IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            var query = GetDataSource(start, end).Where(x => !string.IsNullOrEmpty(x.RefererName));

            var temp = from x in query
                       where string.IsNullOrEmpty(title) || x.RefererKeyword.Contains(title)
                       group x by x.ID into g
                       select new
                       {
                           ID = g.Key,
                           VisitTime = g.Select(x =>
                               x.VisitTime != null ? SqlFunctions.StringConvert((decimal)x.VisitTime.Value.Hour).Trim() + ":"
                                                     + SqlFunctions.StringConvert((decimal)x.VisitTime.Value.Minute).Trim() + ":"
                                                     + SqlFunctions.StringConvert((decimal)x.VisitTime.Value.Second).Trim() : null
                           ),
                           ResourcePage = g.Select(x => x.RefererName),
                           VisitingUrl = g.Select(x => x.VisitingUrl),
                           IP = g.Select(x => x.LocationIP)

                       };
            recordCount = temp.Count();

            return temp.OrderByDescending(x => x.ID).Skip((page - 1) * rows).Take(rows);
        }
    }
}