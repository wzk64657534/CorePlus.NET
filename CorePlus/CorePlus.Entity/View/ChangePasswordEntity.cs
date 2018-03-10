using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CorePlus.Entity
{
    public class ChangePasswordEntity
    {
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }
    }
}