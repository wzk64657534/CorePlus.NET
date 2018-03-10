using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxLocationEvent")]
    public class WxLocationEventEntity : WxEventEntity
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Precision { get; set; }
    }
}
