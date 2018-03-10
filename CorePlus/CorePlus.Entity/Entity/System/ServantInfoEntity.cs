using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;
using Core.Entity;

namespace CorePlus.Entity
{
    [Table("ServantInfo")]
    public class ServantInfoEntity : UserEntity
    {
        [Display(Name = "用户编号")]
        public long? UserId { get; set; }

        [Display(Name = "中文名")]
        public string ChnName { get; set; }

        [Display(Name = "微信商家标识")]
        public string WeiXinNo { get; set; }

        [Display(Name = "OpenId")]
        public string OpenId { get; set; }
    }
}