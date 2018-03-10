using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class ChartDataEntity
    {
        public ChartDataEntity() 
        {
            data = new List<decimal>();
        }
        public string name { get; set; }
        public List<decimal> data { get; set; }
    }
}