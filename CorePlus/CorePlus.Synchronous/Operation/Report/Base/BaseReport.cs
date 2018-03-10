using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CorePlus.API;
using Core;
using System.Threading;
using CorePlus.Entity;

namespace CorePlus.Synchronous
{
    public abstract class BaseReport : IReport
    {
        public void GetReportId(DateTime dt, SynDataInfoEntity entity)
        {
            BaiduV2ReportService baiduReportService
                 = new BaiduV2ReportService(entity.AccountName, CryptHelper.DESDecode(entity.AccountPwd), CryptHelper.DESDecode(entity.Token));

            string dealId = string.Empty;
            while (string.IsNullOrEmpty(dealId))
            {
                dealId = RequestId(baiduReportService, dt);
                Thread.Sleep(500);
            }

            if (string.IsNullOrEmpty(dealId) == false)
            {
                entity.DealWithDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                entity.DealWithType = 3;
                entity.DataType = 2;
                entity.DealWithId = dealId;
                entity.DataTag = GetDataTag();
                DataHelper.AppendSynDataInfo(entity);
            }
        }

        public abstract string RequestId(BaiduV2ReportService baiduReportService, DateTime dt);

        public abstract string GetDataTag();
    }
}