using System;

using CorePlus.API;

namespace CorePlus.Synchronous
{
    public class CreativeReport : BaseReport
    {
        public override string GetDataTag()
        {
            return "creative";
        }

        public override string RequestId(BaiduV2ReportService baiduReportService, DateTime dt)
        {
            return baiduReportService.GetReportForCreative(dt);
        }
    }
}
