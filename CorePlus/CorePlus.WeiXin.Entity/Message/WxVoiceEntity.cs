using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxVoice")]
    public class WxVoiceEntity : WxMessageEntity
    {
        /// <summary>
        /// MediaId
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// Format
        /// </summary>
        public string Format { get; set; }
    }
}