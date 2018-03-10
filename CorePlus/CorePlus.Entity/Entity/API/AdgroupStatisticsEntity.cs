using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("AdgroupStatistics")]
    public class AdgroupStatisticsEntity : StatisticsEntity
    {
        public long? CampaignId { get; set; }
        public long? AdgroupId { get; set; }
    }
}
