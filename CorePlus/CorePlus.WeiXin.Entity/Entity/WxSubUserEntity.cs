using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxSubUser")]
    public class WxSubUserEntity : WxUserEntity
    {
        [Display(Name = "OpenId")]
        public string OpenId { get; set; }

        [Display(Name = "关注/取消")]
        public int? Subscribe { get; set; }

        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Display(Name = "性别")]
        public int? Sex { get; set; }

        [Display(Name = "城市")]
        public string City { get; set; }

        [Display(Name = "国家")]
        public string Country { get; set; }

        [Display(Name = "省份")]
        public string Province { get; set; }

        [Display(Name = "语言")]
        public string Language { get; set; }

        [Display(Name = "头像URL")]
        public string HeadImgUrl { get; set; }

        [Display(Name = "时间戳")]
        public int? SubscribeTime { get; set; }

        [Display(Name = "分组ID")]
        public int? GroupId { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
