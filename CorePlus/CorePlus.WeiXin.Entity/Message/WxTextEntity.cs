using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxText")]
    public class WxTextEntity : WxMessageEntity
    {
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }
    }
}