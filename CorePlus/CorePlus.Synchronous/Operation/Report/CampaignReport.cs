using System;

using CorePlus.API;

namespace CorePlus.Synchronous
{
    public class CampaignReport : BaseReport
    {
        public override string GetDataTag()
        {
            return "campaign";
        }

        public override string RequestId(BaiduV2ReportService baiduReportService, DateTime dt)
        {
            return baiduReportService.GetReportForCampaign(dt);
        }
    }
}
