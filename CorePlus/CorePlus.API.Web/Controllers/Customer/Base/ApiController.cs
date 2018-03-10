using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Core;
using CorePlus.Entity;

namespace CorePlus.API.Web
{
    [Authorize(Roles = "customer")]
    public abstract class ApiController<TRepository, TEntity, TViewStatistics> : SimpleController<TRepository, TEntity>
        where TRepository : SimpleRepository<TEntity>, new()
        where TEntity : BaseEntity, new()
        where TViewStatistics : BaseViewStatisticsEntity, new()
    {
        public override ActionResult Index(long? id, long? arg)
        {
            if (id != null && id != 0)
            {
                ViewBag.HighId = id;
            }

            if (arg != null)
            {
                ViewBag.FolderId = arg;
            }

            return View();
        }

        public override ActionResult Create()
        {
            var entity = Repository.NewEntity(CookieWebHelper.AccountName);
            return View(entity);
        }

        public virtual ActionResult GetStatus()
        {
            int index = GetIndexOfStatus();
            List<ParamDtsInfoEntity> models = ParamWebHelper.GetParamDtsInfo(index);
            models.Insert(0, new ParamDtsInfoEntity { ParamDtsName = "全部状态", ParamValue = "0" });
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        protected virtual int GetIndexOfStatus()
        {
            return 0;
        }

        // 获取分页数据
        public virtual ActionResult GetDataByPager(long? highId, int? page, int? rows, int datepick = 1, string name = null, int state = 0,
            bool isExact = false, int? cost = null, int? price = null, int? clicked = null, long? folderId = null)
        {
            var db = CoreDBContext.GetContext();
            int pageCount = 0;
            int recordCount = 0;
            // 每页行数
            int records = rows ?? ConfigWebHelper.GetDefaultPageSize();
            // 当前页
            int currentPageIndex = page ?? 1;
            // 主数据对象
            List<TEntity> entities = null;
            // 取得监控文件夹下的关键词Id列表，目前只有关键词
            long[] ids = null;
            if (folderId != null)
            {
                ids = db.Set<MonitorInfoEntity>().Where(x => x.FolderId == folderId).Select(x => x.ID).ToArray();
            }
            Type type = typeof(TEntity);
            string tbName = type.Name.Replace("Entity", "");
            string strWhere = GetWhereForSql(highId, ids, name, state, isExact, cost, price, clicked);
            // 通过SQL获取需要的分页数据
            StringBuilder sbEntities = new StringBuilder();
            sbEntities.AppendFormat(" SELECT TOP ({0}) *  FROM {1} AS M ", rows, tbName);
            sbEntities.AppendFormat(" WHERE M.AccountName = '{0}' ", CookieWebHelper.AccountName);
            sbEntities.Append(strWhere);
            sbEntities.AppendFormat(" AND M.ID <= ");
            sbEntities.Append(" ( ");
            sbEntities.Append(" SELECT ISNULL(MIN(ID),0) FROM ");
            sbEntities.Append(" ( ");
            sbEntities.AppendFormat(" SELECT TOP ({0} * ({1} - 1) + 1) ID FROM {2} ORDER BY ID DESC ", rows, currentPageIndex, tbName);
            sbEntities.Append(" ) AS A ");
            sbEntities.Append(" ) ");
            sbEntities.Append(" ORDER BY M.ID DESC ");

            StringBuilder sbCount = new StringBuilder();
            sbCount.AppendFormat(" SELECT COUNT(*) FROM {0} AS M WHERE 1 = 1 ", tbName);
            sbCount.Append(strWhere);

            entities = GetEntityList(new string[] { CryptHelper.DESEncode(sbEntities.ToString()), CryptHelper.DESEncode(sbCount.ToString()) }, out recordCount);
            // 计算总页数
            pageCount = recordCount / records;
            pageCount = recordCount % records > 0 ? pageCount++ : pageCount;

            #region ==== 统计数据 ====
            List<TViewStatistics> models = new List<TViewStatistics>();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    TViewStatistics model = new TViewStatistics();
                    // 装填主数据
                    FillStatistics(item, model);
                    // ==== 汇总 ====
                    // 日期区间
                    DateTime[] dts = GetPeriodOfDate(datepick);
                    ViewStatisticsEntity entity = GetSumOfStatistics(item.ID, dts[0], dts[1]);
                    if (entity != null)
                    {
                        model.AvgClickedPrice = entity.AvgClickedPrice;
                        model.Clicked = entity.Clicked;
                        model.ClickedRate = entity.ClickedRate;
                        model.ShowCnt = entity.ShowCnt;
                        model.ThousandCost = entity.ThousandCost;
                        model.TotalCost = entity.TotalCost;
                        model.TransformCnt = entity.TransformCnt;
                    }
                    else
                    {
                        model.AvgClickedPrice = 0;
                        model.Clicked = 0;
                        model.ClickedRate = 0;
                        model.ShowCnt = 0;
                        model.ThousandCost = 0;
                        model.TotalCost = 0;
                        model.TransformCnt = 0;
                    }

                    models.Add(model);
                }
            }
            #endregion

            var result = new { total = recordCount, rows = models };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // 组装SQL语句的查询条件
        protected virtual string GetWhereForSql(long? highId, long[] ids, string name, int state, bool isExact, int? cost, int? price, int? clicked)
        {
            return string.Empty;
        }

        // 获得分页数据列表
        protected abstract List<TEntity> GetEntityList(string[] sqls, out int records);

        // 装填主数据
        protected abstract void FillStatistics(TEntity entity, TViewStatistics statistics);

        // 获得汇总数据
        protected virtual ViewStatisticsEntity GetSumOfStatistics(long id, DateTime start, DateTime end)
        {
            return null;
        }

        // 获取日期区间
        public DateTime[] GetPeriodOfDate(int i)
        {
            DateTime[] dts = new DateTime[2];
            switch (i)
            {
                case 2:
                    // 本周
                    dts[0] = DateTimeHelper.DateOfWeekBeginning;
                    dts[1] = DateTime.Now;
                    return dts;
                case 3:
                    // 本月
                    dts[0] = Convert.ToDateTime(string.Format("{0}/{1}/01 00:00:00", DateTime.Now.Year, DateTime.Now.Month));
                    dts[1] = DateTime.Now;
                    return dts;
                default:
                    // 昨天
                    dts[0] = DateTimeHelper.Yesterday.Date;
                    dts[1] = DateTime.Now.Date;
                    return dts;
            }
        }

        public string GetRegionTarget(string regionTarget)
        {
            string accountName = CookieWebHelper.AccountName;
            bool b = false;
            var db = CoreDBContext.GetContext();
            AccountInfoEntity account = db.Set<AccountInfoEntity>().Where(x => x.AccountName == accountName).FirstOrDefault();
            if (account != null) { b = Equals(account.RegionTarget, regionTarget); }
            return string.IsNullOrEmpty(regionTarget) ? "没有设置地域" : b ? "账户推广地域" : "计划推广地域";
        }

        public int GetTotalNegativeWords(string[] strArray)
        {
            int i = 0;
            foreach (var item in strArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    i += item.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
            return i;
        }
    }
}