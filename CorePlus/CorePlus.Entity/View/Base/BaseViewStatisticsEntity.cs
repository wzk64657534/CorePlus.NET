using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class BaseViewStatisticsEntity
    {
        public long ID { get; set; }
        public Nullable<long> ShowCnt { get; set; }
        public Nullable<long> Clicked { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<decimal> ClickedRate { get; set; }
        public Nullable<decimal> AvgClickedPrice { get; set; }
        public Nullable<decimal> ThousandCost { get; set; }
        public Nullable<int> TransformCnt { get; set; }
    }
}
