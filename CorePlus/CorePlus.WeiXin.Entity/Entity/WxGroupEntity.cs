using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxGroup")]
    public class WxGroupEntity : WxUserEntity
    {
        [Display(Name = "分组名称")]
        public string Name { get; set; }
    }
}