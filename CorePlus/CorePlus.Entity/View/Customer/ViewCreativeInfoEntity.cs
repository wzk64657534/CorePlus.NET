using System;

namespace CorePlus.Entity
{
    public class ViewCreativeInfoEntity : BaseViewStatisticsEntity
    {
        public string AccountName { get; set; }
        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public long AdgroupId { get; set; }
        public string AdgroupName { get; set; }
        public string Title { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string DestinationUrl { get; set; }
        public string DisplayUrl { get; set; }
        public string Pause { get; set; }
        public string Status { get; set; }
    }
}
