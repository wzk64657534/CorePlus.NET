using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class FileDataBuilder : BaseBuilder
    {
        // 请求服务器生成数据文件
        public override void GetDealWithId()
        {
            LogHelper.AddLog("开始发送处理请求", "GetDealWithId");

            try
            {
                // 取得所有账号
                var accounts = ParamHelper.wcfAccount.FindAll().ToList();
                // 此处请求生成文件
                foreach (var item in accounts)
                {
                    try
                    {
                        SynDataInfoEntity entity = new SynDataInfoEntity()
                        {
                            AccountName = item.AccountName,
                            AccountPwd = item.AccountPwd,
                            Token = item.Token,
                        };

                        DateTime dt = DateTime.Now;
                        var campaigns = ParamHelper.wcfCampaign.FindByExpression(x => x.AccountName == item.AccountName).Count();
                        if (campaigns < 1)
                        {
                            entity.DealWithType = 1;
                            dt = ParamHelper.Current;
                        }
                        else
                        {
                            entity.DealWithType = 2;
                            dt = ParamHelper.PreviousDate;
                        }
                        entity.DealWithDate = dt;
                        entity.DataType = 1;

                        ParamHelper.managerDealWith.GetDealWithId(dt, entity);
                        entity = null;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(ex.Message, item.AccountName, "GetFileId");
                        continue;
                    }
                }
                LogHelper.AddLog(string.Format("发送请求结束，共获得{0}个处理ID", ParamHelper.SynDataMax), "GetFileId");
                // 设置上一次同步的时间
                SettingsHelper.SetPreviousDate(ParamHelper.Current);
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, "GetFileId");
            }
        }
        // 检查文件是否生成，并获取链接，下载文件
        public override void CheckedDealWithId()
        {
            if (ParamHelper.SynDataMax > 0)
            {
                // 发送请求成功后需要等待一些时间，目前设定5分种
                LogHelper.AddLog("等待5分钟，等服务器产生文件", "Run");
                Thread.Sleep(1000 * 60 * 5);
            }

            LogHelper.AddLog(string.Format("开始轮询，待轮询请求{0}个", ParamHelper.SynDataMax), "CheckedDealWithId");
            try
            {
                while (ParamHelper.SynDataMax > 0)
                {
                    var item = ParamHelper.SynDatas[0];

                    SynCheckedDataInfoEntity entity = new SynCheckedDataInfoEntity()
                    {
                        AccountName = item.AccountName,
                        DataTag = item.DataTag,
                        DataType = item.DataType,
                        DealWithDate = item.DealWithDate,
                        DealWithId = item.DealWithId,
                        DealWithType = item.DealWithType,
                        IsDownLoad = false
                    };

                    try
                    {
                        ParamHelper.managerDealWith.CheckedData(entity, item);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(ex.Message, item.AccountName, "CheckedDealWithId", item.DealWithId);
                    }
                    finally
                    {
                        entity = null;
                    }
                }
                LogHelper.AddLog(string.Format("轮询结束，成功的请求{0}个，正在处理的请求{1}个", ParamHelper.SynCheckedDataMax, ParamHelper.SynDataMax), "CheckedDealWithId");
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, "CheckedDealWithId");
            }
        }
        // 同步数据
        public override void SynData()
        {
            LogHelper.AddLog(string.Format("开始同步数据，需要同步{0}个文件", ParamHelper.SynCheckedDataMax), "SynData");
            try
            {
                while (ParamHelper.SynCheckedDataMax > 0)
                {
                    var item = ParamHelper.SynCheckedDatas[0];

                    try
                    {
                        ParamHelper.managerDealWith.SynData(item);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.AddLog(ex.Message, item.AccountName, "SynData", item.DealWithId);
                    }
                }

                LogHelper.AddLog(string.Format("数据同步结束，剩余{0}个文件未同步", ParamHelper.SynCheckedDataMax), "SynData");
            }
            catch (Exception ex)
            {
                LogHelper.AddLog(ex.Message, "SynData");
            }
        }
    }
}