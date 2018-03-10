using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class VisitorActivityRepository : VisitRepository<ResourceClassificationBannerEntity>
    {
        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var entity = base.GetDataOfBanner(start, end);
            var data = GetVisitCount(start, end).Data;
            if (data != null) entity.VISITCOUNT = (int)data;
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


            var list = new List<VisitorDatagridEntity>
            {
                 ProcessData(query, new int?[] { 1, 10 }, "秒",1),
                  ProcessData(query, new int?[] { 11, 30 }, "秒",1),
                   ProcessData(query, new int?[] { 31, 60 }, "秒",1),
                   ProcessData(query, new int?[] { 61, 180 }, "分",60),
                   ProcessData(query, new int?[] { 180, 600 }, "分",60),
                    ProcessData(query, new int?[] { 600, 1800 }, "分",60),
                    ProcessData(query, new int?[] { 1800,null}, "分",60),
            };

            recordCount = list.Count();
            return list.AsQueryable();
        }

        private VisitorDatagridEntity ProcessData(IQueryable<VisitInfoEntity> query, int?[] num, string type, int value)
        {
            string pvPrecent = "0.00%";
            string vcPrecent = "0.00%";

            int? start = num[0];
            int? end = num[1];
            var pvSum = query.Count();
            var vcSum = query.Count(x => x.VisitingSite != x.RefererSite);
            string name;
            Expression<Func<VisitInfoEntity, bool>> countLambda;
            if (num[1] != null)
            {
                name = num[0] / value + "-" + num[1] / value + type;
                countLambda = x => x.VisitPeriodTime > start && x.VisitPeriodTime <= end;
            }
            else
            {
                name = num[0] / value + type + "以上";
                countLambda = x => x.VisitPeriodTime > start;
            }

            var vc = query.Where(x => x.RefererSite != x.VisitingSite).Count(countLambda);
            var pv = query.Count(countLambda);

            if (pvSum != 0)
            {
                pvPrecent = (pv / (decimal)pvSum * 100).ToString("F") + "%";
            }
            if (vcSum != 0)
            {
                vcPrecent = (vc / (decimal)vcSum * 100).ToString("F") + "%";
            }

            var data = new VisitorDatagridEntity { PV = pv, PVPRE = pvPrecent, VC = vc, VCPRE = vcPrecent, NAME = name };
            return data;
        }
    }
}
