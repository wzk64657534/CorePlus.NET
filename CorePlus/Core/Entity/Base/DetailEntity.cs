using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class DetailEntity : BaseEntity
    {
        public DetailEntity()
        {
            SEQ = 0;
        }

        [Display(Name = "序号")]
        public int SEQ { get; set; }
    }
}