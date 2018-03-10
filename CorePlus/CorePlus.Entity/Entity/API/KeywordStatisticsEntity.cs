using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("KeywordStatistics")]
    public class KeywordStatisticsEntity : StatisticsEntity
    {
        public long? CampaignId { get; set; }
        public long? AdgroupId { get; set; }
        public long? KeywordId { get; set; }
        public decimal? AvgRang { get; set; }
    }
}