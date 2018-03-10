using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Core;

namespace CorePlus.WeiXin.Entity
{
    public class WxKeywordEntity : WxUserEntity
    {
        [Required(ErrorMessage = "请输入{0}")]
        [Length(200)]
        [Display(Name = "关键词")]
        public string Keyword { get; set; }

        [Display(Name = "关键词类型")]
        public int? MatchType { get; set; }
    }
}