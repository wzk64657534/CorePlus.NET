using System;
using System.Collections.Generic;
using System.Linq;

using CorePlus.API;
using Core;
using System.Threading;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class DealWithChange : BaseDealWith
    {
        protected override string RequestDealWithId(BaiduV2AccountService serviceAccount, DateTime dt)
        {
            return serviceAccount.GetAllChangedObjects(dt);
        }

        protected override void RequestReport(DateTime dt, SynDataInfoEntity entity)
        {
            // 同时请求统计数据，只取昨天的
            base.RequestStatistics(dt, entity);
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
                    List<FilePathInfo> changes = new List<FilePathInfo>();
                    while (!changes.Any())
                    {
                        changes = serviceAccount.GetAllChangeObjectsPath(item.DealWithId);
                    }

                    if (changes.Any())
                    {
                        foreach (var url in changes)
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
