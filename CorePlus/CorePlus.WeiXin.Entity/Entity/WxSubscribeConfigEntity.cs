using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxSubscribeConfig")]
    public class WxSubscribeConfigEntity : WxUserEntity
    {
        [Display(Name = "默认回复类型")]
        public string ReplyType { get; set; }

        [Display(Name = "是否记录关注者")]
        public bool IsSaveUser { get; set; }

        [Length(500)]
        [DataType(DataType.Url, ErrorMessage = "{0}不符合URL格式")]
        [Display(Name = "OPENID保存链接")]
        public string SaveUrl { get; set; }
    }
}