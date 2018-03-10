using System;
using System.Data.Objects.SqlClient;
using System.Linq;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class SearchEngineRepository : VisitRepository<ResourceClassificationBannerEntity>
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

        //public override IQueryable<VisitInfoEntity> GetDataSource(DateTime start, DateTime end)
        //{
        //    var query = base.GetDataSource(start, end);
        //    return query;
        //}

        protected override IQueryable<ChartItemEntity> LogicDataSourceForXY(IQueryable<VisitInfoEntity> query, DateTime start, DateTime end, int? sourceType)
        {

            bool isDay = Math.Floor((end - start).TotalDays) >= 1;

            if (isDay)
            {
                // 按天
                var day = from x in query
                          where x.RefererType == "1"
                          group x by new
                          {
                              x.RefererName,
                              Year = SqlFunctions.DateName("yyyy", x.VisitTime),
                              Month = SqlFunctions.DateName("MM", x.VisitTime),
                              Day = SqlFunctions.DateName("dd", x.VisitTime),
                          }
                              into g
                              select new ChartItemEntity
                              {
                                  Name = g.Key.RefererName,
                                  Data = g.Count(),

                              };
                return day;
            }
            // 按小时
            var hour = from x in query
                       group x by new
                       {
                           x.RefererName,
                           Year = SqlFunctions.DateName("yyyy", x.VisitTime),
                           Month = SqlFunctions.DateName("MM", x.VisitTime),
                           Day = SqlFunctions.DateName("dd", x.VisitTime),
                           Hour = SqlFunctions.DateName("HH", x.VisitTime),
                       } into g
                       select new ChartItemEntity
                       {
                           Name = g.Key.RefererName,
                           Data = g.Count()
                       };
            return hour;
        }

        public override IQueryable GetDataOfGrid(int page, int rows, DateTime start, DateTime end, int tag, string title, out int recordCount)
        {
            var query = GetDataSource(start, end);
            //var uid = long.Parse(CookieHelper.GetCookie("UserId"));
            //var sitekeyword = (from x in this.DB.Set<UserInfoEntity>()
            //                   where x.ID == uid
            //                   select x.SiteKeyword).FirstOrDefault() ?? "";

            // var ed = end.AddDays(1);
            //var onlyOne = from x in this.DbSet
            //              where (x.VisitTime < ed)
            //              group x by x.VisitId into g
            //              where g.Count() == 1
            //              select new
            //              {
            //                  VisitId = g.Key
            //              };

            var temp = from x in query
                       where x.RefererType == "1" && !string.IsNullOrEmpty(x.RefererName)
                       group x by x.RefererName into g
                       select new
                       {
                           RefererName = g.Key,
                           PV = g.Count(),
                           UV = g.Select(x => x.VisitId).Distinct().Count(),
                           IP = g.Select(x => x.LocationIP).Distinct().Count(),
                           VisitCount = g.Select(x => x.VisitingSite != x.RefererSite).Count(),
                           KeywordCount = g.Select(x => x.RefererKeyword).Distinct().Count()

                       };
            recordCount = temp.Count();
            return temp;
        }
    }
}