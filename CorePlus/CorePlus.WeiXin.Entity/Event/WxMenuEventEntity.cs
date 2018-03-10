using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxMenuEvent")]
    public class WxMenuEventEntity : WxEventEntity
    {
        public string EventKey { get; set; }
    }
}