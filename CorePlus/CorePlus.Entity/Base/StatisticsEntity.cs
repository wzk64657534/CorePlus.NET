using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace CorePlus.Entity
{
    public class StatisticsEntity : BaseEntity
    {
        public Nullable<long> AccountId { get; set; }
        public Nullable<DateTime> SynchroDate { get; set; }
        public Nullable<long> ShowCnt { get; set; }
        public Nullable<long> Clicked { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<decimal> ClickedRate { get; set; }
        public Nullable<decimal> AvgClickedPrice { get; set; }
        public Nullable<decimal> ThousandCost { get; set; }
        public Nullable<int> TransformCnt { get; set; }
        public string ExpandType { get; set; }
    }
}
