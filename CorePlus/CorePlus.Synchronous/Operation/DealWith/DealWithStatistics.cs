using System;
using System.Threading;
using CorePlus.API;
using Core;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public class DealWithStatistics : BaseDealWith
    {
        protected override string GetKeyOfSynData(SynCheckedDataInfoEntity entity)
        {
            return entity.DataTag + "-statistics";
        }

        public override void CheckedData(SynCheckedDataInfoEntity entity, SynDataInfoEntity item)
        {
            BaiduV2ReportService serviceReport
                 = new BaiduV2ReportService(item.AccountName, CryptHelper.DESDecode(item.AccountPwd), CryptHelper.DESDecode(item.Token));

            while (string.IsNullOrEmpty(entity.FilePath))
            {
                Thread.Sleep(1000);
                if (serviceReport.HasReportOnServer(item.DealWithId))
                {
                    FilePathInfo fpi = null;
                    while (fpi == null)
                    {
                        fpi = serviceReport.GetReportFileUrl(item.DealWithId);
                    }

                    if (fpi != null)
                    {
                        entity.FilePath = fpi.FilePath;
                        entity.FileName = fpi.FileName;
                        // 记录获取下载链接的时间，精确到小时
                        entity.DealWithDate = DateTime.Now;
                        // 直接下载
                        base.DownloadFileAndSynData(entity);
                        DataHelper.AppendSynCheckedDataInfo(entity);

                        DataHelper.RemoveSynDataInfo(item);
                    }
                }
            }

            serviceReport = null;
        }
    }
}
