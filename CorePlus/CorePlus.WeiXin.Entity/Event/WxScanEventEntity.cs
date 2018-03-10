using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxScanEvent")]
    public class WxScanEventEntity : WxEventEntity
    {
        public string EventKey { get; set; }
        public string Ticket { get; set; }
    }
}