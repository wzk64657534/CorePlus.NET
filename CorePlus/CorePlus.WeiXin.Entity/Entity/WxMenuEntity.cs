using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxMenu")]
    public class WxMenuEntity : WxUserEntity
    {
        [Required(ErrorMessage = "请输入{0}")]
        [Length(8)]
        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "键值/链接")]
        public string KeyOrUrl { get; set; }

        [Display(Name = "上级菜单ID")]
        public long? HighId { get; set; }
    }
}