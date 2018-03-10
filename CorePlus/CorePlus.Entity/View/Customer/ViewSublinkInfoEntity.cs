using System;

namespace CorePlus.Entity
{
    public class ViewSublinkInfoEntity : BaseViewStatisticsEntity
    {
        public string AccountName { get; set; }
        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public long AdgroupId { get; set; }
        public string AdgroupName { get; set; }
        public string SublinkInfos { get; set; }
        public string Pause { get; set; }
        public string Status { get; set; }
    }
}