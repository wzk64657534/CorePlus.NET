using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("CreativeInfo")]
    public class CreativeInfoEntity : MaterialEntity
    {
        public long? CampaignId { get; set; }

        [Display(Name = "推广单元")]
        public long? AdgroupId { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "创意描述第一行")]
        public string Description1 { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "创意描述第二行")]
        public string Description2 { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(ConstHelper.RegexUrl, ErrorMessage = "格式错误，必须是URL格式，如：http://www.baidu.com")]
        [Display(Name = "访问URL")]
        public string DestinationUrl { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(ConstHelper.RegexUrl, ErrorMessage = "格式错误，必须是URL格式，如：http://www.baidu.com")]
        [Display(Name = "显示URL")]
        public string DisplayUrl { get; set; }

        [Display(Name = "是否启用")]
        public bool Pause { get; set; }

        public int? Status { get; set; }

        public int? Temp { get; set; }
    }
}