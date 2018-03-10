using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("AdgroupInfo")]
    public class AdgroupInfoEntity : MaterialEntity, INameEntity
    {
        [Display(Name = "推广计划")]
        public long? CampaignId { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "推广单元")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [DataType(DataType.Currency, ErrorMessage = "格式错误，只能是货币格式，如：1.23")]
        [Display(Name = "出价")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "否定关键词列表")]
        public string NegativeWords { get; set; }

        [Display(Name = "精确否定关键词列表")]
        public string ExactNegativeWords { get; set; }

        [Display(Name = "是否启用")]
        public bool Pause { get; set; }

        public int? Status { get; set; }
    }
}