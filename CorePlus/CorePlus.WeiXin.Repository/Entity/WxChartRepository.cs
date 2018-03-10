using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Core;
using CorePlus.WeiXin.Entity;
using System.Data.Objects.SqlClient;
using CorePlus.Common;
using CorePlus.Entity;

namespace CorePlus.WeiXin.Repository
{
      public class WxChartRepository : BaseRepository<BaseEntity>
      {
            public List<ChartDataEntity> GetChartMessage(string username, DateTime start, DateTime end)
            {
                  var account = (from x in this.DB.Set<WxAccountEntity>()
                                 where x.UserName == username
                                 select x).First();

                  DateTime endTime = end.AddDays(1);

                  var query = from y in
                                    (
                                          (from x in this.DB.Set<WxTextEntity>()
                                           where x.ToUserName == account.WeiXinNo
                                              && x.RecieveTime >= start
                                              && x.RecieveTime < endTime
                                           select new { Type = "文本", x.RecieveTime, x.ID })
                                     .Union(from x in this.DB.Set<WxImageEntity>()
                                            where x.ToUserName == account.WeiXinNo
                                             && x.RecieveTime >= start
                                             && x.RecieveTime < endTime
                                            select new { Type = "图片", x.RecieveTime, x.ID })
                                       .Union(from x in this.DB.Set<WxRecognitionEntity>()
                                              where x.ToUserName == account.WeiXinNo
                                             && x.RecieveTime >= start
                                             && x.RecieveTime < endTime
                                              select new { Type = "语音", x.RecieveTime, x.ID })
                                    )
                              group y by new
                              {
                                    y.Type,
                                    Year = SqlFunctions.DateName("yyyy", y.RecieveTime),
                                    Month = SqlFunctions.DateName("MM", y.RecieveTime),
                                    Day = SqlFunctions.DateName("d", y.RecieveTime),
                              } into g
                              select new
                              {
                                    DateTime = g.Key.Year + "-" + g.Key.Month + "-" + g.Key.Day,
                                    Name = g.Key.Type,
                                    Data = g.Count()
                              };

                  var list = new List<ChartItemEntity>();
                  string[] sites = new string[] { "文本", "图片", "语音" };

                  //获取日期列表
                  var dir = new Dictionary<string, int>();
                  DateTime temp = start;
                  while (temp <= end)
                  {
                        dir.Add(temp.ToString("yyyy-MM-d"), 0);
                        temp = temp.AddDays(1);
                  }

                  //包含名称的dict
                  var dirDate = sites.ToDictionary(x => x,
                      x => (Dictionary<string, int>)CloneCommonHelper.Clone(dir));
                  foreach (var item in query)
                  {
                        dirDate[item.Name][item.DateTime] = item.Data;
                  }

                  list.AddRange(dirDate.SelectMany(keyValuePair => keyValuePair.Value,
                      (keyValuePair, kv) => new ChartItemEntity { Name = keyValuePair.Key, Data = kv.Value }));

                  List<ChartDataEntity> datas = new List<ChartDataEntity>();
                  foreach (var item in list)
                  {
                        var entity = (from x in datas
                                      where x.name == item.Name
                                      select x).FirstOrDefault();

                        if (entity == null)
                        {
                              ChartDataEntity data = new ChartDataEntity();
                              data.name = item.Name;
                              data.data.Add(item.Data ?? 0);
                              datas.Add(data);
                        }
                        else
                        {
                              entity.data.Add(item.Data ?? 0);
                        }
                  }

                  return datas;
            }

            public List<ChartDataEntity> GetChartSubscribe(string username, DateTime start, DateTime end)
            {
                  var account = (from x in this.DB.Set<WxAccountEntity>()
                                 where x.UserName == username
                                 select x).First();

                  DateTime endTime = end.AddDays(1);
                  var query = from y in
                                    (from x in this.DB.Set<WxSubscribeEventEntity>()
                                     where x.Event == "subscribe"
                                     && x.ToUserName == account.WeiXinNo
                                     select new { Type = "关注", x.RecieveTime, x.ID })
                                        .Union(from x in this.DB.Set<WxSubscribeEventEntity>()
                                               where x.Event == "unsubscribe"
                                               && x.ToUserName == account.WeiXinNo
                                               select new { Type = "取消关注", x.RecieveTime, x.ID })
                              where y.RecieveTime >= start
                              && y.RecieveTime < endTime
                              group y by new
                              {
                                    y.Type,
                                    Year = SqlFunctions.DateName("yyyy", y.RecieveTime),
                                    Month = SqlFunctions.DateName("MM", y.RecieveTime),
                                    Day = SqlFunctions.DateName("d", y.RecieveTime),
                              } into g
                              select new
                              {
                                    DateTime = g.Key.Year + "-" + g.Key.Month + "-" + g.Key.Day,
                                    Name = g.Key.Type,
                                    Data = g.Count()
                              };

                  var list = new List<ChartItemEntity>();
                  string[] sites = new string[] { "关注", "取消关注" };

                  //获取日期dict
                  var dir = new Dictionary<string, int>();
                  DateTime temp = start;
                  while (temp <= end)
                  {
                        dir.Add(temp.ToString("yyyy-MM-d"), 0);
                        temp = temp.AddDays(1);
                  }

                  //包含名称的dict
                  var dirDate = sites.ToDictionary(site => site,
                      site => (Dictionary<string, int>)CloneCommonHelper.Clone(dir));
                  foreach (var item in query)
                  {
                        dirDate[item.Name][item.DateTime] = item.Data;
                  }

                  list.AddRange(dirDate.SelectMany(keyValuePair => keyValuePair.Value,
                      (keyValuePair, kv) => new ChartItemEntity { Name = keyValuePair.Key, Data = kv.Value }));

                  List<ChartDataEntity> datas = new List<ChartDataEntity>();
                  foreach (var item in list)
                  {
                        var entity = (from x in datas
                                      where x.name == item.Name
                                      select x).FirstOrDefault();

                        if (entity == null)
                        {
                              ChartDataEntity data = new ChartDataEntity();
                              data.name = item.Name;
                              data.data.Add(item.Data ?? 0);
                              datas.Add(data);
                        }
                        else
                        {
                              entity.data.Add(item.Data ?? 0);
                        }
                  }

                  return datas;
            }
      }
}