using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("DialogInfo")]
    public class DialogInfoEntity : BaseEntity
    {
        [Display(Name = "对话编号")]
        public string DialogNo { get; set; }
        [Display(Name = "发送者编号")]
        public string FromId { get; set; }
        [Display(Name = "接收者编号")]
        public string ToId { get; set; }
        [Display(Name = "对话时间")]
        public DateTime? DialogTime { get; set; }
        [Display(Name = "对话内容")]
        public string DialogContent { get; set; }
    }
}