using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxLink")]
    public class WxLinkEntity : WxMessageEntity
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}
