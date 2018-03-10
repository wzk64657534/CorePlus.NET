using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxAccount")]
    public class WxAccountEntity : WxUserEntity
    {
        [StringLength(50, ErrorMessage = "超过长度限制，{0}最长不超过{1}个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "Token")]
        public string Token { get; set; }

        [StringLength(50, ErrorMessage = "超过长度限制，{0}最长不超过{1}个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "appid")]
        public string AppId { get; set; }

        [StringLength(50, ErrorMessage = "超过长度限制，{0}最长不超过{1}个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "secret")]
        public string Secret { get; set; }

        [StringLength(50, ErrorMessage = "超过长度限制，{0}最长不超过{1}个字符")]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "grant_type")]
        public string GrantType { get; set; }

        [RegularExpression("^[1-9]([0-9]{3,8})*$", ErrorMessage = "输入不符，请输入不小于1000的整数")]
        [Required(ErrorMessage = "请输入{0}")]
        [Display(Name = "凭证有效时间")]
        public int? ExpiresIn { get; set; }

        [Display(Name = "微信标识")]
        public string WeiXinNo { get; set; }

        [Display(Name = "Token状态")]
        public int? TokenStatus { get; set; }

        [Display(Name = "是否需要记录")]
        public bool IsSaveRecord { get; set; }

        [Display(Name = "是否高级会员")]
        public bool IsAdvanced { get; set; }
    }
}