using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.WeiXin.Entity
{
    [Table("WxRecognition")]
    public class WxRecognitionEntity : WxMessageEntity
    {
        public string MediaId { get; set; }
        public string Format { get; set; }
        public string Recognition { get; set; }
    }
}