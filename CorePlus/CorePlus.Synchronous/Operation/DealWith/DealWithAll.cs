using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class DealWithAll : BaseDealWith
    {
        protected override string RequestDealWithId(BaiduV2AccountService serviceAccount, DateTime dt)
        {
            return serviceAccount.GetAllObjects();
        }

        protected override void RequestReport(DateTime dt, SynDataInfoEntity entity)
        {
            // 同时请求统计数据，默认从2013-1-1开始
            DateTime dtGlobal = Properties.Settings.Default.GlobalDate;
            TimeSpan ts2 = dt - dtGlobal;
            for (int i = 0; i < ts2.TotalDays; i++)
            {
                DateTime paramTime = dtGlobal.AddDays(i);
                base.RequestStatistics(dt, entity);
            }
        }

        protected override string GetKeyOfSynData(SynCheckedDataInfoEntity entity)
        {
            return ParamHelper.GetMaterialKey(entity.FileName) + "-material";
        }

        public override void CheckedData(SynCheckedDataInfoEntity entity, SynDataInfoEntity item)
        {
            BaiduV2AccountService serviceAccount
                 = new BaiduV2AccountService(item.AccountName, CryptHelper.DESDecode(item.AccountPwd), CryptHelper.DESDecode(item.Token));

            while (string.IsNullOrEmpty(entity.FilePath))
            {
                Thread.Sleep(1000);
                if (serviceAccount.HasFileOnServer(item.DealWithId))
                {
                    List<FilePathInfo> alls = new List<FilePathInfo>();
                    while (!alls.Any())
                    {
                        alls = serviceAccount.GetAllObjectsPath(item.DealWithId);
                    }

                    if (alls.Any())
                    {
                        foreach (var url in alls)
                        {
                            entity.FilePath = url.FilePath;
                            entity.FileName = url.FileName;
                            // 记录获取下载链接的时间，精确到小时
                            entity.DealWithDate = DateTime.Now;
                            // 直接下载
                            base.DownloadFileAndSynData(entity);
                            DataHelper.AppendSynCheckedDataInfo(entity);
                        }
                        DataHelper.RemoveSynDataInfo(item);
                    }
                }
            }

            serviceAccount = null;
        }
    }
}