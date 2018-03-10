using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("KeywordInfo")]
    public class KeywordInfoEntity : MaterialEntity
    {
        public long? CampaignId { get; set; }

        [Display(Name = "推广单元")]
        public long? AdgroupId { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "关键词")]
        public string Keyword { get; set; }


        [Required(ErrorMessage = "请输入{0}")]
        [DataType(DataType.Currency, ErrorMessage = "格式错误，必须是货币格式，如：1.23")]
        [Display(Name = "出价")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(ConstHelper.RegexUrl, ErrorMessage = "格式错误，必须是URL格式，如：http://www.baidu.com")]
        [Display(Name = "目标URL")]
        public string DestinationUrl { get; set; }

        [Display(Name = "匹配模式")]
        public int? MatchType { get; set; }

        [Display(Name = "是否启用")]
        public bool Pause { get; set; }

        public int? Status { get; set; }

        public int? Quality { get; set; }

        public int? Temp { get; set; }
    }
}