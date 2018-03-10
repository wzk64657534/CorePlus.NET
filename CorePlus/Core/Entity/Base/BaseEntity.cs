using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class BaseEntity
    {
        [Display(Name = "编号")]
        public long ID { get; set; }
    }
}