using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    public class LoginEntity
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = ("输入不正确，{0}最大长度不超过{1}个英文"))]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "输入不正确，{0}的长度是{1}-{2}位")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "验证码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(4, ErrorMessage = "输入不正确，{0}的长度是{1}位")]
        public string ValidateCode { get; set; }
    }
}