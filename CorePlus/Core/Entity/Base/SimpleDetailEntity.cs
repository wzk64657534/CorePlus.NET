using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class SimpleDetailEntity<TDetail> : BaseEntity
        where TDetail : DetailEntity
    {
        public virtual ICollection<TDetail> Details { get; set; }
    }
}