using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    public class WxUserEntity : BaseEntity
    {
        [Display(Name = "用户名")]
        public string UserName { get; set; }
    }
}
