using System;
using System.Linq;
using System.Linq.Expressions;
using CorePlus.Entity;

namespace CorePlus.Repository
{
    public class TerminalDetailRepository : VisitRepository<ResourceClassificationBannerEntity>
    {
        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var entity = base.GetDataOfBanner(start, end);
            var data = GetNewUV(start, end).Data;
            var count = GetVisitCount(start, end).Data;
            if (data != null) entity.NEWUV = (int) data;

            if (count != null) entity.VISITCOUNT = (int) count;
            return entity;
        }

        public override IQueryable<VisitInfoEntity> GetDataSource(DateTime start, DateTime end)
        {
            var query = base.GetDataSource(start, end);
            return query;
        }

        protected override IQueryable<ChartItemEntity> LogicDataSourceForXY(IQueryable<VisitInfoEntity> query,
            DateTime start, DateTime end, int? sourceType)
        {
            int tag = 0;
            Expression<Func<VisitInfoEntity, string>> whereLambda = null;
            if (sourceType != null)
            {
                tag = (int) sourceType;
            }
            switch (tag)
            {
                case 0:
                    whereLambda = x => x.ConfigOS;
                    break;
                case 1:
                    whereLambda = x => x.ConfigBrowserName;
                    break;
                case 2:
                    whereLambda = x => x.ConfigResolution;
                    break;
                case 3:
                    whereLambda = x => x.ConfigBrowserLang;
                    break;
            }
            if (whereLambda != null)
            {
                var data = query.GroupBy(whereLambda).Select(g => new ChartItemEntity
                {
                    Name = string.IsNullOrEmpty(g.Key) ? "其他" : g.Key,
                    Data = g.Count()
                });
                return data;
            }
            return null;
        }
    }
}
