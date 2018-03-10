using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class ViewStatisticsEntity
    {
        public long ID { get; set; }

        public long ShowCnt { get; set; }

        public long Clicked { get; set; }

        public decimal TotalCost { get; set; }

        public decimal ClickedRate { get; set; }

        public decimal AvgClickedPrice { get; set; }

        public decimal ThousandCost { get; set; }

        public int TransformCnt { get; set; }
    }
}
