using System;

using CorePlus.API;

namespace CorePlus.Synchronous
{
    public class AdgroupReport : BaseReport
    {
        public override string GetDataTag()
        {
            return "adgroup";
        }

        public override string RequestId(BaiduV2ReportService baiduReportService, DateTime dt)
        {
            return baiduReportService.GetReportForAdgroup(dt);
        }
    }
}
