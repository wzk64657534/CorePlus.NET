using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("RoleInfo")]
    public class RoleInfoEntity : BaseEntity
    {
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = "{0}的长度不超过{1}个英文")]
        public virtual string RoleName { get; set; }

        [Display(Name = "中文名")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, ErrorMessage = "{0}的长度不超过{1}个英文")]
        public virtual string ChnName { get; set; }
    }
}