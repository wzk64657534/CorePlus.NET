using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Core;

namespace CorePlus.Entity
{
    [Table("CampaignInfo")]
    public class CampaignInfoEntity : MaterialEntity, INameEntity
    {
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "计划名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "预算")]
        public decimal? Budget { get; set; }

        [Display(Name = "推广地域")]
        public string RegionTarget { get; set; }

        [Display(Name = "IP排除列表")]
        public string ExcludeIp { get; set; }

        [Display(Name = "否定关键词列表")]
        public string NegativeWords { get; set; }

        [Display(Name = "精确否定关键词列表")]
        public string ExactNegativeWords { get; set; }

        public string Schedule { get; set; }

        public string BudgetOfflineTime { get; set; }

        [Display(Name = "创意展现方式")]
        public int? ShowProb { get; set; }

        [Display(Name = "是否启用")]
        public bool Pause { get; set; }

        public bool JoinContent { get; set; }

        public decimal? ContentPrice { get; set; }

        public int? Status { get; set; }

        [NotMapped]
        public string[] RegionList { get; set; }
    }
}