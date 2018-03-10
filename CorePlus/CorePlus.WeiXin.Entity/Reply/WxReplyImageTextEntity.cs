using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Core;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxReplyImageText")]
    public class WxReplyImageTextEntity : WxKeywordEntity
    {
        public WxReplyImageTextEntity()
        {
            Selected = false;
        }

        [Required(ErrorMessage = "请输入{0}")]
        [Length(50)]
        [Display(Name = "标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "图文封面")]
        public string PicUrl { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Length(500)]
        [Display(Name = "描述")]
        public string Description { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Length(500)]
        [DataType(DataType.Url, ErrorMessage = "不符合URL格式")]
        [Display(Name = "跳转URL")]
        public string Url { get; set; }

        [Length(500)]
        [Display(Name = "多图文")]
        public string WithIds { get; set; }

        [NotMapped]
        public string[] Ids { get; set; }

        [NotMapped]
        public bool Selected { get; set; }
    }
}