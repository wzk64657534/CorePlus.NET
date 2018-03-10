using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("MenuInfo")]
    public class MenuInfoEntity : BaseEntity
    {
        public MenuInfoEntity()
        {
            IsToUser = false;
        }

        [Display(Name = "菜单名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = "{0}长度不超过{1}个英文字符")]
        public string MenuName { get; set; }

        [Display(Name = "菜单控制器")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, ErrorMessage = "{0}长度不超过{1}个英文字符")]
        public string Controller { get; set; }

        [Display(Name = "菜单行为")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, ErrorMessage = "{0}长度不超过{1}个英文字符")]
        public string Action { get; set; }

        [Display(Name = "菜单链接")]
        public string Link { get; set; }

        [Display(Name = "菜单图标")]
        public string IconName { get; set; }

        [Display(Name = "用户菜单")]
        public bool IsToUser { get; set; }

        [NotMapped]
        public virtual bool Selected { get; set; }
    }
}