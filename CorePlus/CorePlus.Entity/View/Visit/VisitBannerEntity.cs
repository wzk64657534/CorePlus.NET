using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorePlus.Entity
{
    public class VisitBannerEntity
    {
        // 浏览量
        public int PV { get; set; }
        // 访客数
        public int UV { get; set; }
        // 独立IP数量
        public int IP { get; set; }
        // 人均浏览页数
        public decimal PVAVG { get; set; }
        // 全站总PV
        public decimal TOTALPV { get; set; }
        // 浏览次数占比
        public decimal PVPERCENT { get; set; }
        // 页面停留总时长
        public string TOTALTIME { get; set; }
        // 页面停留平均时长
        public string AVGTIME { get; set; }
        // 备注
        public string REMARK { get; set; }
    }
}