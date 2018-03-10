using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Core;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxReplyText")]
    public class WxReplyTextEntity : WxKeywordEntity
    {
        [Required(ErrorMessage = "请输入{0}")]
        [Length(200)]
        [Display(Name = "内容")]
        public string Content { get; set; }
    }
}