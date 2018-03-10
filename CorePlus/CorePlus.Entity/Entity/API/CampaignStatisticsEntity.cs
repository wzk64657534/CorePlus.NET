using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("CampaignStatistics")]
    public class CampaignStatisticsEntity : StatisticsEntity
    {
        public long? CampaignId { get; set; }
    }
}