using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using CorePlus.Common;
using CorePlus.Entity;


namespace CorePlus.Repository
{
    public class ResourceClassificationRepository : VisitRepository<ResourceClassificationBannerEntity>
    {

        protected override IQueryable<ChartItemEntity> LogicDataSourceForXY(IQueryable<VisitInfoEntity> query, DateTime start, DateTime end, int? sourceType)
        {

            bool isDay = Math.Floor((end - start).TotalDays) >= 1;
            string[] sites;
            var list = new List<ChartItemEntity>();
            if (isDay)
            {
                // 按天
                var day = from x in query
                          group x by new
                          {
                              x.RefererType,
                              Year = SqlFunctions.DateName("yyyy", x.VisitTime),
                              Month = SqlFunctions.DateName("MM", x.VisitTime),
                              Day = SqlFunctions.DateName("dd", x.VisitTime),
                          }
                              into g
                              select new
                              {
                                  DateTime = g.Key.Year + "-" + g.Key.Month + "-" + g.Key.Day,
                                  Name = g.Key.RefererType == "0" ? "直接输入" : "搜索引擎",
                                  Data = g.Count()
                              };

                list.Clear();
                sites = (day.Select(x => x.Name)).Distinct().ToArray();

                //获取日期dict
                var dir = new Dictionary<string, int>();
                DateTime temp = start;
                while (temp <= end)
                {
                    dir.Add(temp.ToString("yyyy-MM-d"), 0);
                    temp = temp.AddDays(1);
                }

                //包含名称的dict
                var dirDate = sites.ToDictionary(site => site, site => (Dictionary<string, int>)CloneCommonHelper.Clone(dir));
                foreach (var item in day)
                {
                    if (dirDate.ContainsKey(item.Name))
                        dirDate[item.Name][item.DateTime] = item.Data;
                }

                list.AddRange(dirDate.SelectMany(keyValuePair => keyValuePair.Value,
                    (keyValuePair, kv) => new ChartItemEntity { Name = keyValuePair.Key, Data = kv.Value }));

                #region 修改成Linq前代码
                //foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair in dirDate)
                //{

                //    foreach (KeyValuePair<string, int> kv in keyValuePair.Value)
                //    {
                //        var chartItem = new ChartItemEntity { Name = keyValuePair.Key, Data = kv.Value };

                //        list.Add(chartItem);
                //    }

                //}
                #endregion

                #region 第一版
                //获取日期数组
                //var datetime = new List<string>();
                //DateTime temp = start;
                //while (temp <= end)
                //{
                //    datetime.Add(temp.ToString("yyyy-MM-dd"));
                //    temp = temp.AddDays(1);
                //}

                //foreach (var site in sites)
                //{
                //    foreach (var date in datetime)
                //    {
                //        var now = (from x in day
                //                   where x.DateTime == date
                //                         && x.Name == site
                //                   select x).FirstOrDefault();

                //        var item = new ChartItemEntity();
                //        if (now == null)
                //        {
                //            item.Name = site;
                //            item.Data = 0;
                //        }
                //        else
                //        {
                //            item.Name = now.Name;
                //            item.Data = now.Data;
                //        }
                //        list.Add(item);
                //    }
                //}

                #endregion
                return list.AsQueryable();
            }
            else
            {
                // 按小时
                var nows = from x in query
                           group x by new
                           {
                               x.RefererType,
                               Year = SqlFunctions.DatePart("yyyy", x.VisitTime),
                               Month = SqlFunctions.DatePart("MM", x.VisitTime),
                               Day = SqlFunctions.DatePart("dd", x.VisitTime),
                               Hour = SqlFunctions.DatePart("HH", x.VisitTime),
                           }
                               into g
                               select new
                               {
                                   g.Key.Hour,
                                   Name = g.Key.RefererType == "0" ? "直接输入" : "搜索引擎",
                                   Data = g.Count()
                               };
                list.Clear();

                var dirHours = new Dictionary<int, int>();
                int time = DateTime.Now.Hour;
                if (start != DateTime.Now.Date)
                {
                    time = 23;
                }
                for (int i = 0; i <= time; i++)
                {
                    dirHours.Add(i, 0);
                }
                sites = (from x in nows select x.Name).Distinct().ToArray();

                var dirDate = sites.ToDictionary(site => site,
                    site => (Dictionary<int, int>)CloneCommonHelper.Clone(dirHours));
                foreach (var item in nows)
                {
                    dirDate[item.Name][(int)item.Hour] = item.Data;
                }

                list.AddRange(dirDate.SelectMany(keyValuePair => keyValuePair.Value,
                    (keyValuePair, kv) => new ChartItemEntity { Name = keyValuePair.Key, Data = kv.Value }));

                #region 第一版
                //List<int> hours;
                //if (start == DateTime.Now.Date)
                //{
                //    int time = DateTime.Now.Hour;
                //    hours = new List<int>();
                //    for (int i = 0; i <= time; i++)
                //    {
                //        hours.Add(i);
                //    }
                //}
                //else
                //{
                //    hours = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
                //}

                //sites = (from x in nows select x.Name).Distinct().ToArray();

                //foreach (var site in sites)
                //{
                //    foreach (var hour in hours)
                //    {
                //        var now = (from x in nows
                //            where x.Hour == hour
                //                  && x.Name == site
                //            select x).FirstOrDefault();

                //        var item = new ChartItemEntity();
                //        if (now == null)
                //        {
                //            item.Name = site;
                //            item.Data = 0;
                //        }
                //        else
                //        {
                //            item.Name = now.Name;
                //            item.Data = now.Data;
                //        }
                //        list.Add(item);
                //    }
                //}
                #endregion
                return list.AsQueryable();
            }
        }
        public override ResourceClassificationBannerEntity GetDataOfBanner(DateTime start, DateTime end)
        {
            var entity = base.GetDataOfBanner(start, end);

            var data = GetNewUV(start, end).Data;
            var count = GetVisitCount(start, end).Data;

            if (count != null) entity.VISITCOUNT = (int)count;
            if (data != null) entity.NEWUV = (int)data;

            return entity;
        }
    }

}