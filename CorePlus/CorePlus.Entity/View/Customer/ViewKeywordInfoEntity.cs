using System;

namespace CorePlus.Entity
{
    public class ViewKeywordInfoEntity : BaseViewStatisticsEntity
    {
        public string AccountName { get; set; }
        public long CampaignId { get; set; }
        public string CampaignName { get; set; }
        public long AdgroupId { get; set; }
        public string AdgroupName { get; set; }
        public string Keyword { get; set; }
        public decimal Price { get; set; }
        public string DestinationUrl { get; set; }
        public string MatchType { get; set; }
        public string Pause { get; set; }
        public string Status { get; set; }
        public string Quality { get; set; }
    }
}