using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("MonitorInfo")]
    public class MonitorInfoEntity : BaseEntity
    {
        public long? FolderId { get; set; }
        public long? ObjId { get; set; }
        public long? AdgroupId { get; set; }
        public long? CampaignId { get; set; }
        public int? Type { get; set; }
    }
}
