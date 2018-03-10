using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CorePlus.Entity
{
    [Table("AccountInfo")]
    public class AccountInfoEntity : BaseEntity
    {
        [Display(Name = "用户编号")]
        public virtual long UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }        

        [Display(Name = "账户编号")]
        [Required(ErrorMessage = "*")]
        [Remote("CheckAccountId", "Account")]
        public virtual long AccountId { get; set; }

        [Display(Name = "账户登录名")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "{0}长度不能超过20个字符")]
        [Remote("CheckAccountName", "Account", ErrorMessage = "此{0}已存在")]
        public virtual string AccountName { get; set; }

        [Display(Name = "账户中文名")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "{0}长度不能超过20个字符")]

        public virtual string AccountChnName { get; set; }

        [Display(Name = "账户密码")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "输入不正确，{0}的长度是{2}-{1}位")]
        public virtual string AccountPwd { get; set; }

        [Display(Name = "密钥")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "输入不正确，{0}的长度是{2}-{1}位")]
        public virtual string Token { get; set; }

        [Display(Name = "最后更新时间")]
        public DateTime? LastUpdateTime { get; set; }        

        [Display(Name = "账户预算")]
        [Range(typeof(decimal), "50.00", "300000.00", ErrorMessage = "输入不正确，{0}的区间是是{1}-{2}位")]
        public virtual decimal? Budget { get; set; }

        [Display(Name = "账户累计消费")]
        public virtual decimal? Cost { get; set; }

        [Display(Name = "账户投资")]
        public virtual decimal? Payment { get; set; }

        [Display(Name = "账户余额")]
        public virtual decimal? Balance { get; set; }

        [Display(Name = "账户类型")]
        public virtual int? CategoryValue { get; set; }

        [Display(Name = "目标区域")]
        public string RegionTarget { get; set; }

        [Display(Name = "禁止IP")]
        public string ExcludeIp { get; set; }

        [Display(Name = "账户财务数据类型")]
        public int? Type { get; set; }

        [Display(Name = "账户开放域名列表")]
        public string OpenDomains { get; set; }
    }
}
