using System;

using CorePlus.API;

namespace CorePlus.Synchronous
{
    public class KeywordReport : BaseReport
    {
        public override string GetDataTag()
        {
            return "keyword";
        }

        public override string RequestId(BaiduV2ReportService baiduReportService, DateTime dt)
        {
            return baiduReportService.GetReportForKeyword(dt);
        }
    }
}
