using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class ViewAdgroupInfoEntity : BaseViewStatisticsEntity
    {
        public string AccountName { get; set; }

        public long CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string AdgroupName { get; set; }

        public decimal MaxPrice { get; set; }

        public string NegativeWords { get; set; }

        public string ExactNegativeWords { get; set; }

        public string Pause { get; set; }

        public string Status { get; set; }
    }
}
