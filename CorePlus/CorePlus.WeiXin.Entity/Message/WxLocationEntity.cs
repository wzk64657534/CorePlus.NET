using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxLocation")]
    public class WxLocationEntity : WxMessageEntity
    {
        /// <summary>
        /// Location_X
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// Location_Y
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// Scale
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// Label
        /// </summary>
        public string Label { get; set; }
    }
}
