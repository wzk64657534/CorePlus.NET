using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("CreativeStatistics")]
    public class CreativeStatisticsEntity : StatisticsEntity
    {
        public long? CampaignId { get; set; }
        public long? AdgroupId { get; set; }
        public long? CreativeId { get; set; }
        public decimal? AvgRang { get; set; }
    }
}