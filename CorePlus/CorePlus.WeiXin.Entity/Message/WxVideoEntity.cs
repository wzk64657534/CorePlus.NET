using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxVideo")]
    public class WxVideoEntity : WxMessageEntity
    {
        /// <summary>
        /// MediaId
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// ThumbMediaId
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
