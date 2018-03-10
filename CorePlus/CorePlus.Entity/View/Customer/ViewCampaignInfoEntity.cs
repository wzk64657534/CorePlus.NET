using System;

namespace CorePlus.Entity
{
    public class ViewCampaignInfoEntity : BaseViewStatisticsEntity
    {
        public string AccountName { get; set; }

        public string CampaignName { get; set; }

        public string Budget { get; set; }

        public string RegionTarget { get; set; }

        public string ExcludeIp { get; set; }

        public string NegativeWords { get; set; }

        public string ExactNegativeWords { get; set; }

        public string Schedule { get; set; }

        public string BudgetOfflineTime { get; set; }

        public string ShowProb { get; set; }

        public string Pause { get; set; }

        public string JoinContent { get; set; }

        public string ContentPrice { get; set; }

        public string Status { get; set; }
    }
}