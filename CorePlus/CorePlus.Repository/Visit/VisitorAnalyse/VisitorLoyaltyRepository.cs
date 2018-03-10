using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class VisitorLoyaltyRepository : VisitRepository<ResourceClassificationBannerEntity>
    {
        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var entity = base.GetDataOfBanner(start, end);
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
            var data = from x in query

                       group x by x.VisitId
                           into g
                           select new VisitorDatagridEntity
                           {
                               NAME = g.Key,
                               Count = g.Count(),

                           };
            var pvSum = query.Count();
            var uvSum = data.Count();

            var list = new List<VisitorDatagridEntity>();
            for (int i = 1; i < 7; i++)
            {
                var datas = ProcessData(data, query, pvSum, uvSum, i);
                list.Add(datas);
            }
            recordCount = list.Count();
            return list.AsQueryable();
        }

        private VisitorDatagridEntity ProcessData(IQueryable<VisitorDatagridEntity> data, IQueryable<VisitInfoEntity> query, int pvSum, int uvSum, int num)
        {

            string pvPrecent = "0%";
            string uvPrecent = "0%";

            string name = num.ToString(CultureInfo.InvariantCulture) + "次";
            var uv = data.Count(x => x.Count == num);
            var ids = data.Where(x => x.Count == num).Select(x => x.NAME);
            if (num == 1)
            {
                name = "只访问了一次";
            }
            else if (num >= 6)
            {
                name = "访问了六次以上";
                uv = data.Count(x => x.Count > num);
                ids = data.Where(x => x.Count > num).Select(x => x.NAME);
            }
            var pv = (from x in query
                      where ids.Contains(x.VisitId)
                      select x).Count();
            if (pv != 0)
            {
                pvPrecent = (pv / (decimal)pvSum * 100).ToString("F") + "%";
            }
            if (uv != 0)
            {
                uvPrecent = (uv / (decimal)uvSum * 100).ToString("F") + "%";
            }

            var datas = new VisitorDatagridEntity { NAME = name, PV = pv, UV = uv, PVPRE = pvPrecent, UVPRE = uvPrecent };
            return datas;
        }
    }
}
