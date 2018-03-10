using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace Core.Entity
{
    public class UserEntity : BaseEntity
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = ("输入不正确，{0}最大长度不超过{1}个英文"))]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string UserPwd { get; set; }

        [Display(Name = "角色编号")]
        public long? RoleId { get; set; }

        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
    }
}
