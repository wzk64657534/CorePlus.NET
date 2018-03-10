using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.WeiXin.Entity
{
    public class WxEventEntity : WxEntity
    {
        public WxEventEntity() : base() { }
        public string Event { get; set; }
    }
}