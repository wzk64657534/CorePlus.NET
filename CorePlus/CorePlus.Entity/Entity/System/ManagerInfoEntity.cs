using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;
using Core.Entity;

namespace CorePlus.Entity
{
    [Table("ManagerInfo")]
    public class ManagerInfoEntity : UserEntity
    {
        [Display(Name = "中文名")]
        public virtual string ChnName { get; set; }

        [Display(Name = "电子邮箱")]
        [DataType(DataType.EmailAddress, ErrorMessage = "格式不正确")]
        [StringLength(50, ErrorMessage = "输入不正确，{0}的长度不超过{1}位")]
        public virtual string Email { get; set; }

        [Display(Name = "座机号码")]
        public virtual string Tel { get; set; }

        [Display(Name = "手机号码")]
        public virtual string Phone { get; set; }

        [Display(Name = "最后一次登录时间")]
        public virtual DateTime? LastLoginTime { get; set; }
    }
}