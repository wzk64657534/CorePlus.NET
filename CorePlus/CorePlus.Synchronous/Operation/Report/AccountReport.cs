using System;

using CorePlus.API;

namespace CorePlus.Synchronous
{
    public class AccountReport : BaseReport
    {
        public override string GetDataTag()
        {
            return "account";
        }

        public override string RequestId(BaiduV2ReportService baiduReportService, DateTime dt)
        {
            return baiduReportService.GetReportForAccount(dt);
        }
    }
}