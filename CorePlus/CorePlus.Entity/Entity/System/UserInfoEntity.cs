using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Core;
using Core.Entity;

namespace CorePlus.Entity
{
    [Table("UserInfo")]
    public class UserInfoEntity : UserEntity
    {
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression("^[\u4E00-\u9FA5]+$", ErrorMessage = "输入不正确，{0}只能是中文")]
        [Display(Name = "中文名")]
        [Length(20)]
        public string ChnName { get; set; }

        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "用户类型")]
        public Nullable<int> UserType { get; set; }

        [Display(Name = "用户状态")]
        public Nullable<int> UserStatus { get; set; }

        [Display(Name = "座机号码")]
        public string Tel { get; set; }

        [Display(Name = "手机号码")]
        public string Phone { get; set; }

        [Display(Name = "最后一次登录时间")]
        public Nullable<DateTime> LastLoginTime { get; set; }

        [Display(Name = "用户级别")]
        public Nullable<int> UserLevel { get; set; }

        [Display(Name = "站点关键词")]
        public string SiteKeyword { get; set; }
    }
}