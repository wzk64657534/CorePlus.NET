using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.ComponentModel.DataAnnotations;

namespace CorePlus.Entity
{
    [Table("LogInfo")]
    public class LogInfoEntity : BaseEntity
    {
        public string AccountName { get; set; }
        public string ErrorMsg { get; set; }
        public string Operation { get; set; }
        public Nullable<DateTime> ErrorDate { get; set; }
        public string Tag { get; set; }
    }
}