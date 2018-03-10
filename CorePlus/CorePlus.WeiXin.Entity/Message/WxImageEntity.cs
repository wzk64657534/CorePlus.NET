using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxImage")]
    public class WxImageEntity : WxMessageEntity
    {
        /// <summary>
        /// MediaId
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// PicUrl
        /// </summary>
        public string PicUrl { get; set; }
    }
}
