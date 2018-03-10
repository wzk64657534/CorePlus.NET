using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("SublinkInfo")]
    public class SublinkInfoEntity : MaterialEntity
    {
        public long? CampaignId { get; set; }

        [Display(Name = "推广单元")]
        public long? AdgroupId { get; set; }

        public string SublinkInfos { get; set; }

        [Display(Name = "是否启用")]
        public bool Pause { get; set; }

        public int? Status { get; set; }

        public int? Temp { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "蹊径子链文字描述")]
        public string Description { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "访问URL")]
        public string DescriptionUrl { get; set; }
    }
}